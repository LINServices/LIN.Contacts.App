using LIN.Contacts.Shared.Drawers;
using LIN.Contacts.Shared.Modals;
using LIN.Types.Contacts.Models;
using LIN.Types.Responses;
using Microsoft.AspNetCore.Components;

namespace LIN.Contacts.App.Components.Pages;


public partial class Index
{


    private DevicesDrawer Devices;




    /// <summary>
    /// Emma.
    /// </summary>
    private EmmaDrawer? EmmaIA { get; set; }


    /// <summary>
    /// Nuevo contacto.
    /// </summary>
    private ContactDrawer? NewContact { get; set; }


    /// <summary>
    /// Editor
    /// </summary>
    private ContactEdit? ContactEditDrawer { get; set; }


    /// <summary>
    /// Patron de búsqueda.
    /// </summary>
    private string Pattern { get; set; } = string.Empty;



    /// <summary>
    /// Obtiene si los proyectos ya están cargados
    /// </summary>
    public static Index Me { get; set; } = null!;



    /// <summary>
    /// Obtiene si los proyectos ya están cargados
    /// </summary>
    public static bool AreProjectLoaded { get; set; }



    /// <summary>
    /// OLista de proyectos
    /// </summary>
    public static List<ContactModel> Contactos { get; set; } = [];



    /// <summary>
    /// Lista de renderizado.
    /// </summary>
    public static List<IGrouping<char, ContactModel>> RenderList { get; set; } = new();



    /// <summary>
    /// Modal de contacto.
    /// </summary>
    public static ContactModal? ModalContactos { get; set; }




    /// <summary>
    /// Abrir Emma.
    /// </summary>
    private void OpenEmma() => EmmaIA?.Show();



    /// <summary>
    /// Buscar.
    /// </summary>
    /// <param name="e"></param>
    public void Search(ChangeEventArgs e)
    {

        // Patron de búsqueda.
        Pattern = e.Value?.ToString() ?? string.Empty;

        // Patron vacío.
        if (string.IsNullOrWhiteSpace(Pattern))
        {
            // Lista original.
            RenderList = Contactos.GroupBy(contact => contact.Nombre[0]).ToList();

            // Estado.
            StateHasChanged();

            return;
        }


        // Buscar.
        string pattern = Pattern.Normalize().Trim().ToLower();

        // Consulta.
        RenderList = (from contacto in Contactos
                      where contacto.Nombre.Contains(pattern, StringComparison.CurrentCultureIgnoreCase)
                      || contacto.Mails.Where(T => T.Email.ToLower().Contains(pattern)).Any()
                      || contacto.Phones.Where(T => T.Number.ToLower().Contains(pattern)).Any()
                      select contacto).GroupBy(t => t.Nombre[0]).ToList();

        // Nuevo estado.
        StateHasChanged();

    }



    /// <summary>
    /// Ir a una ruta.
    /// </summary>
    /// <param name="url">Ruta.</param>
    public void GoTo(string url) => nav.NavigateTo(url);



    /// <summary>
    /// Abrir drawer de crear.
    /// </summary>
    private void OpenCreate() => NewContact?.Show();


    /// <summary>
    /// Método de inicio
    /// </summary>
    protected override async Task OnInitializedAsync()
    {

        // Obtiene los proyectos
        if (!AreProjectLoaded)
            await LoadContacts();

        // base
        await base.OnInitializedAsync();

    }


    /// <summary>
    /// Carga la lista de proyectos
    /// </summary>
    public async Task LoadContacts()
    {

        AreProjectLoaded = false;

        // Obtiene los contactos.
        var result = await Access.Contacts.Controllers.Contacts.ReadAll(Access.Contacts.Session.Instance.Token);

        // Evalúa el resultado
        if (result.Response == Responses.Success)
        {
            AreProjectLoaded = true;
            Contactos = result.Models;
            RenderList = result.Models.GroupBy(t => t.Nombre[0].ToString().ToUpper()[0]).ToList();
            StateHasChanged();
        }

        var local = await GetLocalContacts();
        Contactos.AddRange(local);
        RenderList = result.Models.GroupBy(t => t.Nombre[0].ToString().ToUpper()[0]).ToList();
        StateHasChanged();

    }

    public async static Task<List<ContactModel>> GetLocalContacts()
    {

        try
        {

            var x = new Thread(async () =>
            {
                var contacts = await Microsoft.Maui.ApplicationModel.Communication.Contacts.Default.GetAllAsync();
                List<ContactModel> models = [];
                foreach (var contact in contacts)
                {
                    ContactModel model = new()
                    {
                        Nombre = contact.DisplayName,
                        Type = Types.Contacts.Enumerations.ContactTypes.None,
                        Phones = contact.Phones.Select(t => new PhoneModel()
                        {
                            Number = t.PhoneNumber,
                        }).ToList(),
                        Mails = contact.Emails.Select(t => new MailModel()
                        {
                            Email = t.EmailAddress,
                        }).ToList()
                    };
                    models.Add(model);
                }

                Contactos.AddRange(models);
                try
                {
                    await Me.InvokeAsync(Me.StateHasChanged);
                }
                catch (Exception ex)
                {
                }
            });
            x.Start();
        }
        catch
        {

        }
        return [];




    }



    public void Add(ContactModel model)
    {
        this.InvokeAsync(() =>
        {

            var exist = Contactos.Where(t => t.Id == model.Id).Any();

            if (exist)
                return;

            Contactos.Add(model);
            RenderList = Contactos.GroupBy(t => t.Nombre[0].ToString().ToUpper()[0]).ToList();
            StateHasChanged();
        });
    }





    /// <summary>
    /// Al crear correctamente un contacto.
    /// </summary>
    async void OnSuccess()
    {

        // Vuelve a cargar las llaves
        await LoadContacts();

        StateHasChanged();

    }




    /// <summary>
    /// Al crear correctamente un contacto.
    /// </summary>
    async void OnEdit(ContactModel model)
    {

        ContactEditDrawer?.Show(model);
    }



    public void Refresh()
    {
        this.InvokeAsync(() =>
        {

            RenderList = Contactos.GroupBy(t => t.Nombre[0].ToString().ToUpper()[0]).ToList();
            StateHasChanged();

        });
    }




    void OnSend(ContactModel contact)
    {

        // Nuevo onInvoque.
        Devices.OnInvoke = (e) =>
        {
            LIN.Contacts.Shared.Online.Realtime.InventoryAccessHub.SendToDevice(e.Id, new()
            {
                Command = $"viewContact({contact.Id})"
            });
        };

        Devices.Show();
    }




    void Filter(Types.Contacts.Enumerations.ContactTypes type)
    {

        if (type == Types.Contacts.Enumerations.ContactTypes.None)
        {
            Refresh();
            return;
        }


        RenderList = Contactos.Where(t => t.Type == type).GroupBy(t => t.Nombre[0].ToString().ToUpper()[0]).ToList();
        StateHasChanged();

    }


    async void Close()
    {
        var db = new LIN.LocalDataBase.Data.UserDB();
        await db.DeleteUsers();

        LIN.Access.Contacts.Session.CloseSession();

        nav.NavigateTo("/login", true);
    }





}