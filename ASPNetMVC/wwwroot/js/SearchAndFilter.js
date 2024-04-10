document.addEventListener('DOMContentLoaded', function () {
    var input = document.getElementById('search-input');

    if (input) {

        input.addEventListener('input', function () {

            var searchText = this.value.toLowerCase();
            var courses = document.querySelectorAll('.course');

            courses.forEach(function (course) {

                var courseTitle = course.querySelector('h4').textContent.toLowerCase();

                if (courseTitle.includes(searchText)) {
                    course.style.display = 'block';
                } else {
                    course.style.display = 'none';
                }
            });
        });
    }
});

document.addEventListener('DOMContentLoaded', function () {
    var input = document.getElementById('filter');

    if (input) {

        input.addEventListener('input', function () {

            var filterValue = this.value;
            var courses = document.querySelectorAll('.course');

            courses.forEach(function (course) {

                var tags = course.querySelectorAll('.tag');   
                var match = false;

                var match = false;
                tags.forEach(function (tag) {
                    if (tag.classList.contains(filterValue)) {
                        match = true;
                    }
                });

                if (filterValue === 'all categories' || match) {
                    course.style.display = 'block';
                } else {
                    course.style.display = 'none';
                }
            });
        });
    }
});