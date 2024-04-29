
if (document.querySelector(".user_icon")) {
    document.querySelector(".user_icon").addEventListener("click", function () {
        document.querySelector(".modalsaveprof").style.display = "block";

    })
}
function closemodel() {
    document.querySelector(".modalsaveprof").style.display = "none";

}
document.querySelector(".formsaves").addEventListener("click", function (e) {
    e.stopPropagation();
})

