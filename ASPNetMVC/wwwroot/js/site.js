document.addEventListener('DOMContentLoaded', function () {
    let sw = document.querySelector('#switch-mode')

    sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light"

        fetch(`/settings?mode=${theme}`)
            .then(res => {
                if (res.ok)
                    window.location.reload()
            })
    })
})

const toggleMenu = () => {
    document.getElementById('menu').classList.toggle('hide')
    document.getElementById('buttons').classList.toggle('hide')
    document.getElementById('button').classList.toggle('hide')
    document.getElementById('switch').classList.toggle('hide')
}

const checkSize = () => {
    if (window.innerWidth >= 1200) {
        document.getElementById('menu').classList.remove('hide')
        document.getElementById('buttons').classList.remove('hide')
        document.getElementById('button').classList.remove('hide')
        document.getElementById('switch').classList.remove('hide')
    } else {
        if (!document.getElementById('menu').classList.contains('hide')) {
            document.getElementById('menu').classList.toggle('hide')
        }
        if (!document.getElementById('buttons').classList.contains('hide')) {
            document.getElementById('buttons').classList.toggle('hide')
        }
        if (!document.getElementById('switch').classList.contains('hide')) {
            document.getElementById('switch').classList.toggle('hide')
        }
        if (!document.getElementById('button').classList.contains('hide')) {
            document.getElementById('button').classList.toggle('hide')
        }
    }
}

window.addEventListener('resize', checkSize);
checkSize();
