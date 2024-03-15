const isDarkMode = localStorage.getItem('darkMode') === 'true';
const logo = document.getElementById('logoImg');

function setDarkMode(isDark) {
    const link = document.getElementById('stylesheet');
    if (isDark) {
        link.setAttribute('href', "/css/Site-Dark.min.css");
        logo.src = "/images/silicon-logo-dark_theme.svg";
    } else {
        link.setAttribute('href', "/css/Site.min.css");
        logo.src = "/images/silicon-logo-light_theme.svg"; 
    }

    document.getElementById('switch-mode').checked = isDark;
}

function toggleDarkMode() {
    const isDark = localStorage.getItem('darkMode') === 'true';
    localStorage.setItem('darkMode', (!isDark).toString());
    setDarkMode(!isDark);
}

function initializeDarkMode() {
    if (isDarkMode !== null) {
        setDarkMode(isDarkMode);
    }
}

window.addEventListener('load', initializeDarkMode);
