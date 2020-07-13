var prevrowIndex;
document.addEventListener('click', function (args) {
    if (args.target.classList.contains("e-dtdiagonalright") || args.target.classList.contains("e-detailrowcollapse")) {
        var gObj = document.getElementsByClassName("e-grid")[0].blazor__instance;
        if (prevrowIndex != undefined) {
            var rowele = gObj.getRowByIndex(prevrowIndex).querySelector(".e-detailrowexpand");
            rowele.click();
        }
        prevrowIndex = parseInt(args.target.closest("tr").getAttribute("aria-rowindex"));
    }
})