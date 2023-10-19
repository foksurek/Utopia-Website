const button = document.getElementById("nav-mobile");

button.addEventListener("click", () => {
    if ($(".nav-elements").css("display") == "none") {
        $(".menu-mobile").css("transform", "rotate(180deg)")
        $(".nav-elements").css("display", "flex");
    }
    else {
        $(".nav-elements").removeAttr('style');
    }
});