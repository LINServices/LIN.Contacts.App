// @ts-check

// Extensiones para flowbite


// Abre un Drawer
function ShowDrawer(id, ...idCloseBtn)
{

    // Control
    const control = document.getElementById(id);

    const closeButton = document.getElementById(idCloseBtn);

    const options = {
        backdropClasses: 'bg-zinc-900/20 dark:bg-black/80 fixed inset-0 z-30'
    };

    const drawer = new Drawer(control, options);

    drawer.show();

    for (let i = 0; i < idCloseBtn.length; i++) {

        try {

            let closeButton = document.getElementById(idCloseBtn[i]);


            closeButton.addEventListener("click", () => {

                drawer.hide();

            });
        }
        catch
        {

        }

    }

}






// Abre un Drawer
function ShowBottomDrawer(id, ...idCloseBtn) {

    // Control
    const control = document.getElementById(id);

    const options = {
        placement: "bottom",
        backdropClasses: 'bg-zinc-900 bg-opacity-50 fixed inset-0 z-30',
        onHide: () => {
        },
        onShow: () => {
        }
    };


    const drawer = new Drawer(control, options);

    drawer.show();

    for (let i = 0; i < idCloseBtn.length; i++) {

        try {

            let closeButton = document.getElementById(idCloseBtn[i]);

            closeButton.addEventListener("click", () => {

                drawer.hide();

            });
        } catch {

        }

    }

}








// Abre
function ForceClick(id)
{

    const control = document.getElementById(id);
    control.click();
}





function ShowPop(id, btn )
{
    // set the popover content element
    const $targetEl = document.getElementById(id);

    // set the element that trigger the popover using hover or click
    const $triggerEl = document.getElementById(btn);

    // options with default values
    const options = {
        placement: 'bottom',
        triggerType: 'hover',
        offset: 10,
        onHide: () => {
            console.log('popover is shown');
        },
        onShow: () => {
            console.log('popover is hidden');
        },
        onToggle: () => {
            console.log('popover is toggled');
        }
    };

    const popover = new Popover($targetEl, $triggerEl, options);

    popover.show();
}






// Abre un

// Abre
function ShowModal(id, ...idCloseBtn) {

    // Control
    const control = document.getElementById(id);



    // options with default values
    const options = {
        placement: 'center',
        backdrop: 'dynamic',
        backdropClasses:
            'bg-zinc-900/40 dark:bg-black/80 fixed inset-0 z-40',
        closable: true,
        onHide: () => {
            console.log('modal is hidden');
        },
        onShow: () => {
            console.log('modal is shown');
        },
        onToggle: () => {
            console.log('modal has been toggled');
        },
    };

  
    const drawer = new Modal(control, options);

    // show the drawer
    drawer.show();


    for (let i = 0; i < idCloseBtn.length; i++) {

        try {
            let closeButton = document.getElementById(idCloseBtn[i]);


            closeButton.addEventListener("click", () => {

                drawer.hide();

            });
        }
        catch
        {

        }
       
    }

   

}






function OpenDropDown(id, idOpen, ...idCloseBtn) {



    // set the target element that will be collapsed or expanded (eg. navbar menu)
    const $targetEl = document.getElementById(id);
    const $targetEl2 = document.getElementById(idOpen);

    // options with default values
    const options = {
        placement: 'bottom',
        triggerType: 'click',
        offsetSkidding: 0,
        offsetDistance: 10,
        delay: 100,
        onHide: () => {
            console.log('dropdown has been hidden');
        },
        onShow: () => {
            console.log('dropdown has been shown');
        },
        onToggle: () => {
            console.log('dropdown has been toggled');
        }
    };

    let collapse = new Dropdown($targetEl, $targetEl2, options);



    for (let i = 0; i < idCloseBtn.length; i++) {

        try {
            let closeButton = document.getElementById(idCloseBtn[i]);


            closeButton.addEventListener("click", () => {

                collapse.hide();

            });
        }
        catch
        {

        }

    }


    // show the target element
    collapse.toggle();



}








function E() {



    // set the target element that will be collapsed or expanded (eg. navbar menu)
    const $targetEl = document.getElementById('user-dropdown');

    const $targetEl2 = document.getElementById('user-menu-button');


    // options with default values
    const options = {
        placement: 'bottom',
        triggerType: 'click',
        offsetSkidding: 0,
        offsetDistance: 10,
        delay: 100,
        onHide: () => {
            console.log('dropdown has been hidden');
        },
        onShow: () => {
            console.log('dropdown has been shown');
        },
        onToggle: () => {
            console.log('dropdown has been toggled');
        }
    };

    let collapse = new Dropdown($targetEl, $targetEl2, options);

    // show the target element
    collapse.toggle();



}


function I() {


    // set the target element that will be collapsed or expanded (eg. navbar menu)
    const $targetEl = document.getElementById('mobile-menu-2');


    const collapse1 = new Collapse($targetEl, $targetEl);

    // show the target element
    collapse1.toggle();

}





function enviarCorreo(mail) {
    var mailtoURL = "mailto:" + mail;

    // Abrir el cliente de correo predeterminado del usuario
    window.location.href = mailtoURL;
}


function llamar(phone) {
    var mailtoURL = "tel:" + phone;

    // Abrir el cliente de correo predeterminado del usuario
    window.location.href = mailtoURL;
}