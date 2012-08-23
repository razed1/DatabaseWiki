(function() {
  window.columnDescriptionContainer = (function() {
    function columnDescriptionContainer(thePassedInElement) {
      var proto;
      if (!(thePassedInElement != null)) {
        throw new Error("dragAndDropList: No element was passed in.");
      }
      proto = columnDescriptionContainer.prototype;
      thePassedInElement = addTo(thePassedInElement, proto.columnDescriptionContainerCreator, {});
      proto.addUpdateDescriptionMethodTo(thePassedInElement);
      proto.addHideMethodTo(thePassedInElement);
    }
    columnDescriptionContainer.prototype.addHideMethodTo = function(element) {
      return element[0].hideFieldset = function(result) {
        var fieldSet;
        fieldSet = jQuery(element).find(".fieldsetColumnDescriptionText");
        return fieldSet.hide();
      };
    };
    columnDescriptionContainer.prototype.addUpdateDescriptionMethodTo = function(element) {
      return element[0].updateDescription = function(result) {
        var fieldSet;
        fieldSet = jQuery(element).find(".fieldsetColumnDescriptionText");
        fieldSet.find(".divDescriptionText").text(result.Description);
        fieldSet.find("#legend").text(result.ColumnName);
        fieldSet.show();
        return;
      };
    };
    columnDescriptionContainer.prototype.columnDescriptionContainerCreator = function() {
      return fieldset({
        "class": "fieldsetColumnDescriptionText",
        style: "display:none;"
      }, function() {
        legend({
          id: "legend"
        }, function() {
          return "";
        });
        return div({
          "class": "divDescriptionText"
        }, function() {
          return "";
        });
      });
    };
    return columnDescriptionContainer;
  })();
}).call(this);
