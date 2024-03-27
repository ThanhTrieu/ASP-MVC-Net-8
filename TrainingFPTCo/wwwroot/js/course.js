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
                    self.text("Delete");
                    if (response.cod == 200) {
                        alert(response.message);
                        // An bo dong vua xoa
                        $('.row-course-' + idCourse).hide();
                    } else {
                        alert(response.message);
                        return;
                    }
                }
            });
        }
    })
});