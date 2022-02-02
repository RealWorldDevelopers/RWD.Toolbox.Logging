
/*sample calls that include logging calls to logging.js*/
/*TODO set mvc app in folder 06 in serilog 1 */

function buttonClick() {
   alert('clicked!');

   var addtlInfo = {
      customValue: "Something extra"
   };
   logUsage("clicked button", addtlInfo);
}

function getAllTodos() {
   perfLoggerStart("getting all todos");
   $("#content").empty();
   $.ajax({
      method: "GET",
      url: "https://sampleapi.knowyourtoolset.com/api/Todos"
   }).done(function (data) {
      $.each(data, function (ind, val) {
         $("#content").append("<li style=\"font-size: 20px;\">" + val.Item + "</li>");
      });
      perfLoggerStop("getting all todos");
   });
}

function getNum2() {
   logDiagnostic("starting get", { todo_id: 2 });
   $("#content2").empty();
   $.ajax({
      method: "GET",
      url: "https://sampleapi.knowyourtoolset.com/api/Todos/2",
   }).done(function (data) {
      $("#content2").append("<li style=\"font-size: 20px;\">" + data.Item +
         " (" + data.Completed + ")</li>");
      logDiagnostic("api call completed", { todo_id: 2 });
   });
}

function updateNum2() {
   $("#content3").empty();
   var todo = {
      Item: $("#UpdatedText").val(),
      Completed: false
   }

   $.ajax({
      method: "PUT",
      url: "https://sampleapi.knowyourtoolset.com/api/Todos/2",
      contentType: "application/json",
      data: JSON.stringify(todo)
   }).done(function (data) {
      $("#content3").append("<div class=\"alert alert-success\">Successful update!</div>");
   }).fail(function (response) {
      logError("failed updating todo", response.responseText, todo);
      $("#content3").append("<div class=\"alert alert-danger\">"
         + response.responseText + "</div>");
   });
}