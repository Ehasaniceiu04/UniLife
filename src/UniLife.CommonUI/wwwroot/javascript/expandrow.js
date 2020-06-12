var prevrowIndex;
document.addEventListener('click', function (args) {
    if (args.target.classList.contains("e-dtdiagonalright") || args.target.classList.contains("e-detailrowcollapse")) {
        var gObj = document.getElementById("OgrGrid").ej2_instances[0];
        if (prevrowIndex != undefined) {
            gObj.detailRowModule.collapse(prevrowIndex);
        }
        prevrowIndex = parseInt(args.target.closest("tr").getAttribute("aria-rowindex"));
    }
})