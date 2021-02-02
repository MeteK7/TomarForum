function openTab(evt, tabName) {
    var i, tabContentAbout, tabLinks;
    tabContentAbout = document.getElementsByClassName("tab-content-about");
    for (i = 0; i < tabContentAbout.length; i++) {
        tabContentAbout[i].style.display = "none";
    }
    tabLinks = document.getElementsByClassName("tab-links");
    for (i = 0; i < tabLinks.length; i++) {
        tabLinks[i].className = tabLinks[i].className.replace(" active", "");
    }
    document.getElementById(tabName).style.display = "block";
    evt.currentTarget.className += " active";
}

// Get the element with id="defaultOpen" and click on it. It is for opening a tab defaultly.
document.getElementById("defaultOpen").click();