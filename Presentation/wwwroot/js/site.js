window.addEventListener('resize', removeSidebarShowOnResize);
document.addEventListener('DOMContentLoaded', () => {
    initDarkMode()
    initCloseButtons()
    initMobileMenu()
    initModals()
    initNotificationsMenuDropDown()
    initProfileOptionsDropDown()
    initSettingsMenuDropDown()
    initToggles()

    updateRelativeTimes();
    updateTimeRemaining();
    setInterval(updateRelativeTimes, updateTimeRemaining, 60000);
})

function initMobileMenu() {
    const buttons = document.querySelectorAll('[data-type="menu"]')
    buttons.forEach(button => {
        button.addEventListener('click', () => {
            const target = button.getAttribute('data-target')
            const targetElement = document.querySelector(target)

            targetElement.classList.add('show')
        })
    })
}

function initModals() {
    const buttons = document.querySelectorAll('[data-type="modal"]')
    buttons.forEach(button => {
        button.addEventListener('click', () => {
            const target = button.getAttribute('data-target')
            const targetElement = document.querySelector(target)
            console.log("TARGET: ", targetElement)
            if (targetElement) {
                
                targetElement.classList.add('show')
            }
        })
    })
}

function initCloseButtons() {
    const buttons = document.querySelectorAll('[data-type="close"]')
    buttons.forEach(button => {
        button.addEventListener('click', () => {
            const target = button.getAttribute('data-target')
            const targetElement = document.querySelector(target)

            if (targetElement) {
                targetElement.classList.remove('show')
            }
        })
    })
}

function initProfileOptionsDropDown() {
    const avatar = document.getElementById('profileAvatar')
    const dropdown = document.getElementById('profileDropdown')

    avatar.addEventListener('click', function () {
        dropdown.classList.toggle('show')
    });

    document.addEventListener('click', function (e) {
        if (!avatar.contains(e.target) && !dropdown.contains(e.target)) {
            dropdown.classList.remove('show')
        }
    });
}

function initSettingsMenuDropDown() {
    const icon = document.getElementById('settingsIcon')
    const dropdown = document.getElementById('settingsDropdown')

    icon.addEventListener('click', function () {
        dropdown.classList.toggle('show')
    });

    document.addEventListener('click', function (e) {
        if (!icon.contains(e.target) && !dropdown.contains(e.target)) {
            dropdown.classList.remove('show')
        }
    });
}

function initNotificationsMenuDropDown() {
    const icon = document.getElementById('notificationsIcon')
    const dropdown = document.getElementById('notificationsDropdown')

    icon.addEventListener('click', function () {
        dropdown.classList.toggle('show')
    });

    document.addEventListener('click', function (e) {
        if (!icon.contains(e.target) && !dropdown.contains(e.target)) {
            dropdown.classList.remove('show')
        }
    });
}


function removeSidebarShowOnResize() {
    const sidebar = document.getElementById('sidebar')
    if (sidebar && sidebar.classList.contains('show')) {
        sidebar.classList.remove('show')
    }
}


function initDarkMode() {
    if (localStorage.getItem('theme') === 'dark') {
        document.body.classList.add('dark-mode')
        document.documentElement.setAttribute('data-mode', 'dark')

        const darkModeToggle = document.querySelector('#darkModeToggle')
        darkModeToggle.checked = true
    }
}

function initToggles() {
    const toggles = document.querySelectorAll('[data-type="toggle"]')

    toggles.forEach(toggle => {
        const togglefunction = toggle.getAttribute('data-func')

        if (togglefunction === "darkmode") {
            toggle.addEventListener('change', function () {
                if (this.checked) {
                    document.documentElement.setAttribute('data-mode', 'dark')
                    localStorage.setItem('theme', 'dark')
                } else {
                    document.documentElement.removeAttribute('data-mode')
                    localStorage.setItem('theme', 'light')
                }
            });
        }
    })
}

function updateRelativeTimes() {
    const elements = document.querySelectorAll('.time');
    const now = new Date();

    elements.forEach(el => {
        const created = new Date(el.getAttribute('data-created'));
        const diff = now - created;
        const diffSeconds = Math.floor(diff / 1000);
        const diffMinutes = Math.floor(diffSeconds / 60);
        const diffHours = Math.floor(diffMinutes / 60);
        const diffDays = Math.floor(diffHours / 24);
        const diffWeeks = Math.floor(diffDays / 7);

        let relativeTime = '';

        if (diffMinutes < 1) {
            relativeTime = 'Just now';
        } else if (diffMinutes < 60) {
            relativeTime = diffMinutes + ' min ago';
        } else if (diffHours < 2) {
            relativeTime = diffHours + ' hour ago';
        } else if (diffHours < 24) {
            relativeTime = diffHours + ' hours ago';
        } else if (diffDays < 2) {
            relativeTime = diffDays + ' day ago';
        } else if (diffDays < 7) {
            relativeTime = diffDays + ' days ago';
        } else {
            relativeTime = diffWeeks + ' weeks ago';
        }
        el.textContent = relativeTime;
    });
}

function updateTimeRemaining() {
    const elements = document.querySelectorAll('.time');
    const now = new Date();
    console.log('Current time:', now); 

    elements.forEach(el => {
        const endDateStr = el.getAttribute('data-enddate');
        console.log('End date from attribute:', endDateStr);

        const endDate = new Date(endDateStr);
        console.log('Parsed end date:', endDate);

        if (isNaN(endDate)) {
            console.error("Invalid end date:", endDateStr);
            return;
        }

        const diff = endDate - now;
        const diffSeconds = Math.floor(diff / 1000);
        const diffMinutes = Math.floor(diffSeconds / 60);
        const diffHours = Math.floor(diffMinutes / 60);
        const diffDays = Math.floor(diffHours / 24);
        const diffWeeks = Math.floor(diffDays / 7);
        const diffMonths = Math.floor(diffDays / 30);
        const diffYears = Math.floor(diffDays / 365);

        let remainingTime = '';

        if (diff < 0) {
            remainingTime = 'The event has passed';
        } else if (diffYears >= 1) {
            remainingTime = `In ${diffYears} year${diffYears > 1 ? 's' : ''}`;
        } else if (diffMonths >= 1) {
            remainingTime = `In ${diffMonths} month${diffMonths > 1 ? 's' : ''}`;
        } else if (diffWeeks >= 1) {
            remainingTime = `In ${diffWeeks} week${diffWeeks > 1 ? 's' : ''} left`;
        } else if (diffDays >= 1) {
            remainingTime = `In ${diffDays} day${diffDays > 1 ? 's' : ''} left`;
        } else if (diffHours >= 1) {
            remainingTime = `In ${diffHours} hour${diffHours > 1 ? 's' : ''} left`;
        } else if (diffMinutes >= 1) {
            remainingTime = `In ${diffMinutes} minute${diffMinutes > 1 ? 's' : ''} left`;
        } else {
            remainingTime = `In ${diffSeconds} second${diffSeconds > 1 ? 's' : ''} left`;
        }

        el.textContent = remainingTime;
    });
}

