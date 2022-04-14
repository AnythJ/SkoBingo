function showNavbar() {
    document.getElementById("sideNavbar").style.width = "15rem";
    document.getElementById("closeIcon").style.transform = "rotate(90deg)";
}

function hideNavbar() {
    document.getElementById("sideNavbar").style.width = "0";
    document.getElementById("closeIcon").style.transform = "rotate(-90deg)";
}