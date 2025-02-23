using LIN.Emma.UI.Classes;
using LIN.Types.Responses;
using LIN.Access.Contacts;

namespace LIN.Contacts.App.Online;


internal class Realtime
{


    /// <summary>
    /// Iniciar el servicio.
    /// </summary>
    public static void Start()
    {
        // Iniciar Hub.
        LIN.Contacts.Shared.Online.Realtime.Start();
    }



    /// <summary>
    /// Construye las funciones.
    /// </summary>
    public static void Build()
    {

        SILFFunction addContact = new(async (values) =>
        {

            // Obtener el parámetro.
            var value = values.FirstOrDefault(t => t.Name == "id")?.Objeto.GetValue();

            // Validar el tipo.
            if (value is not decimal)
                return;

            // Id.
            var id = (int)((value as decimal?) ?? 0);


            if (!LIN.Contacts.App.Components.Pages.Index.AreProjectLoaded)
                return;


            var contact = await LIN.Access.Contacts.Controllers.Contacts.Read(id, Session.Instance.Token);

            LIN.Contacts.App.Components.Pages.Index.Me.Add(contact.Model);


        })
        // Propiedades
        {
            Name = "addContact",
            Parameters =
            [
                new("id", new("number")),
            ]
        };



        SILFFunction removeContact = new(async (values) =>
        {

            // Obtener el parámetro.
            var value = values.FirstOrDefault(t => t.Name == "id")?.Objeto.GetValue();

            // Validar el tipo.
            if (value is not decimal)
                return;

            // Id.
            var id = (int)((value as decimal?) ?? 0);


            if (!LIN.Contacts.App.Components.Pages.Index.AreProjectLoaded)
                return;


            var count = Components. Pages.Index.Contactos.RemoveAll(x => x.Id == id);



            if (count > 0)
            {
                Components.Pages.Index.Me.Refresh();
            }

        })
        // Propiedades
        {
            Name = "removeContact",
            Parameters =
           [
               new("id", new("number")),
            ]
        };


        SILFFunction updateContact = new(async (values) =>
        {

            // Obtener el parámetro.
            var value = values.FirstOrDefault(t => t.Name == "id")?.Objeto.GetValue();

            // Validar el tipo.
            if (value is not decimal)
                return;

            // Id.
            var id = (int)((value as decimal?) ?? 0);


            if (!LIN.Contacts.App.Components.Pages.Index.AreProjectLoaded)
                return;


            var contact = await LIN.Access.Contacts.Controllers.Contacts.Read(id, Session.Instance.Token);


            if (contact.Response != Responses.Success)
                return;


            var get = Components.Pages.Index.Contactos.Where(t => t.Id == contact.Model.Id).FirstOrDefault();

            if (get == null)
                return;


            get.Phones = contact.Model.Phones;
            get.Mails = contact.Model.Mails;
            get.Name = contact.Model.Name;
            get.Picture = contact.Model.Picture;
            get.Type = contact.Model.Type;
            get.Birthday = contact.Model.Birthday;


            Components.Pages.Index.Me.Refresh();


        })
        // Propiedades
        {
            Name = "update",
            Parameters =
           [
               new("id", new("number")),
            ]
        };




        SILFFunction viewContact = new(async (values) =>
        {

            // Obtener el parámetro.
            var value = values.FirstOrDefault(t => t.Name == "id")?.Objeto.GetValue();

            // Validar el tipo.
            if (value is not decimal)
                return;

            // Id.
            var id = (int)((value as decimal?) ?? 0);



            var modelo = Components.Pages.Index.Contactos.Where(t => t.Id == id).FirstOrDefault();


            if (modelo == null)
            {
                var contact = await LIN.Access.Contacts.Controllers.Contacts.Read(id, Session.Instance.Token);

                if (contact.Response == Responses.Success)
                    modelo = contact.Model;

            }

            if (modelo == null)
            {
                return;
            }




            Components.Pages.Index.ModalContactos.Show(modelo);


        })
        // Propiedades
        {
            Name = "viewContact",
            Parameters =
         [
             new("id", new("number")),
            ]
        };


        // Guardar métodos.
        LIN.Contacts.Shared.Online.Realtime.Build([addContact, removeContact, updateContact, viewContact]);

    }



}