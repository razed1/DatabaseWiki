(function() {
  window.columnDescriptionContainer = (function() {
    function columnDescriptionContainer(thePassedInElement, editColumnDescriptionMethod) {
      var idBag, proto;
      if (!(thePassedInElement != null)) {
        throw new Error("dragAndDropList: No element was passed in.");
      }
      idBag = {
        buttonEditDescriptionName: "buttonEditColumnDecription",
        buttonSaveDescriptionName: "buttonSaveColumnDescription",
        divDescriptionTextName: "divDescriptionText",
        divDataTypeName: "divDataType",
        divEditDescriptionTextName: "divEditColumnDescriptionTextName",
        fieldSetName: "fieldsetColumnDescriptionText",
        hiddenIdName: "hiddenColumnIdName",
        legendName: "legend",
        textareaEditDescriptionName: "textareaEditColumnDescriptionName"
      };
      proto = columnDescriptionContainer.prototype;
      thePassedInElement = addTo(thePassedInElement, proto.columnDescriptionContainerCreator, {
        idNames: idBag
      });
      proto.addUpdateDescriptionMethodTo(thePassedInElement, idBag);
      window.addHideMethodTo(thePassedInElement, idBag);
      window.setButtonClicks(thePassedInElement, idBag, proto, editColumnDescriptionMethod);
    }
    /* Description */
    columnDescriptionContainer.prototype.addUpdateDescriptionMethodTo = function(element, idNames) {
      return element[0].updateDescription = function(result) {
        var description, fieldSet;
        description = result.Description != null ? result.Description : "";
        fieldSet = jQuery(element).find("#" + idNames.fieldSetName);
        fieldSet.find("#" + idNames.divDescriptionTextName).text(description);
        fieldSet.find("#" + idNames.textareaEditDescriptionName).val(description);
        fieldSet.find("#" + idNames.legendName).text(result.ColumnName);
        fieldSet.find("#" + idNames.divDataTypeName).text("Data Type: " + result.DataType);
        fieldSet.find("#" + idNames.hiddenIdName).val(result.ColumnId);
        setStateToView(fieldSet, idNames);
        fieldSet.show();
        return;
      };
    };
    /* End Description */
    columnDescriptionContainer.prototype.columnDescriptionContainerCreator = function() {
      return fieldset({
        id: this.idNames.fieldSetName,
        "class": this.idNames.fieldSetName,
        style: "display:none;"
      }, function() {
        legend({
          id: this.idNames.legendName
        }, function() {
          return "";
        });
        div({
          id: this.idNames.divDescriptionTextName,
          "class": this.idNames.divDescriptionTextName
        }, function() {
          return "";
        });
        div({
          id: this.idNames.divEditDescriptionTextName,
          style: "display:none;"
        }, function() {
          return textarea({
            id: this.idNames.textareaEditDescriptionName
          });
        });
        div(function() {
          button({
            id: this.idNames.buttonEditDescriptionName,
            type: "button"
          }, function() {
            return "edit";
          });
          return button({
            id: this.idNames.buttonSaveDescriptionName,
            type: "button",
            style: "display:none;"
          }, function() {
            return "save";
          });
        });
        div({
          id: this.idNames.divDataTypeName,
          "class": this.idNames.divDataTypeName
        }, function() {
          return "";
        });
        return div(function() {
          return input({
            type: "hidden",
            id: this.idNames.hiddenIdName
          });
        });
      });
    };
    return columnDescriptionContainer;
  })();
}).call(this);
