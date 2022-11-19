$(document).ready(function () {
    ActiveNav();
});

function ActiveNav() {
    var subMenus = document.getElementsByClassName("menu-item menu-item-submenu menu-item-open menu-item-here");

    for (const subMenu of subMenus) {
        subMenu.classList.remove("menu-item-open");
        subMenu.classList.remove("menu-item-here");
    }
    var home = document.querySelector("#kt_aside_menu > ul > li.menu-item.menu-item-active");
    home.classList.remove("menu-item-active");

    var activeLink = document.querySelector('a[href="' + window.location.pathname + '"]');
    activeLink.parentElement.classList.add("menu-item-active");
    activeLink.parentElement.parentElement.parentElement.parentElement.classList.add("menu-item-open", "menu-item-here");

}