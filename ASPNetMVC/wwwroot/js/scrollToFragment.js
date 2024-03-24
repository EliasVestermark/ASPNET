window.onload = function () {
    var fragment = getFragmentFromQueryString();
    if (fragment) {
        var element = document.querySelector(fragment);
        if (element) {
            element.scrollIntoView({ behavior: "instant", block: "start" });
        }
    }
};

function getFragmentFromQueryString() {
    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    return params.get("fragment");
}
