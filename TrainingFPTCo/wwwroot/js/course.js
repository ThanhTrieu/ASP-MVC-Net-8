$(function () {
    $(".js-delete-course").on("click", function () {
        let self = $(this);
        let idCourse = self.attr("id").trim();
        if ($.isNumeric(idCourse)) {
            $.ajax({
                url: "/Courses/Delete",
                type: "post",
                data: { id: idCourse },
                beforeSend: function () {
                    self.text("Loading ...");
                },
                success: function (response) {
                    console.log(response);
                    self.text("Delete");
                }
            });
        }
    })
});