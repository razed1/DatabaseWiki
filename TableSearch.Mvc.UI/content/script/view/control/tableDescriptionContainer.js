(function() {
  window.tableDescriptionContainer = (function() {
    function tableDescriptionContainer(thePassedInElement, columnDescriptionMethod, editTableDescriptionMethod) {
      var idBag, proto;
      if (!(thePassedInElement != null)) {
        throw new Error("dragAndDropList: No element was passed in.");
      }
      proto = tableDescriptionContainer.prototype;
      idBag = {
        buttonEditDescriptionName: "buttonEditTableDecription",
        buttonSaveDescriptionName: "buttonSaveTableDescription",
        columnButtonPrefix: "buttonColumnItem_",
        divColumnButtonHolderName: "divColumnButtonHolder",
        divDescriptionTextName: "divDescriptionText",
        divEditDescriptionTextName: "divEditDescriptionText",
        fieldSetName: "fieldsetTableDescriptionText",
        hiddenIdName: "hiddenTableId",
        legendName: "legend",
        tableHeaderName: "tableDescriptionHeader",
        textareaEditDescriptionName: "textareaEditDescription"
      };
      thePassedInElement = addTo(thePassedInElement, proto.tableDescriptionContainerCreator, {
        idNames: idBag
      });
      proto.addUpdateDescriptionMethodTo(thePassedInElement, proto.createSetColumnButtonsMethod(idBag, proto), columnDescriptionMethod, window.setStateToView, idBag);
      window.addHideMethodTo(thePassedInElement, idBag);
      window.setButtonClicks(thePassedInElement, idBag, proto, editTableDescriptionMethod);
      thePassedInElement;
    }
    /* Table Description Area Methods */
    tableDescriptionContainer.prototype.addUpdateDescriptionMethodTo = function(element, createButtons, columnDescriptionMethod, setStateToView, idNames) {
      return element[0].updateDescription = function(result) {
        var description, fieldSet;
        description = result.Description != null ? result.Description : "";
        fieldSet = jQuery(element).find("#" + idNames.fieldSetName);
        fieldSet.find("#" + idNames.divDescriptionTextName).text("" + description);
        fieldSet.find("#" + idNames.tableHeaderName).text("[" + result.DatabaseName + "." + result.SchemaName + "]");
        fieldSet.find("#" + idNames.legendName).text("" + result.TableName);
        fieldSet.find("#" + idNames.textareaEditDescriptionName).val(description);
        fieldSet.find("#" + idNames.hiddenIdName).val(result.Id);
        createButtons(fieldSet, result.ColumnList, columnDescriptionMethod);
        setStateToView(fieldSet, idNames);
        fieldSet.show();
        return;
      };
    };
    tableDescriptionContainer.prototype.tableDescriptionContainerCreator = function() {
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
          id: this.idNames.tableHeaderName,
          "class": this.idNames.tableHeaderName
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
          id: this.idNames.divColumnButtonHolderName
        }, function() {
          return "";
        });
        return div(function() {
          return input({
            id: this.idNames.hiddenIdName,
            type: "hidden"
          });
        });
      });
    };
    /* Column Button Methods */
    tableDescriptionContainer.prototype.columnButtonCreator = function() {
      var item, _i, _len, _ref, _results;
      _ref = this.columnList;
      _results = [];
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        _results.push(button({
          id: "" + this.itemPrefix + item.Id,
          "class": "columnNameButton"
        }, function() {
          return "" + item.ColumnName;
        }));
      }
      return _results;
    };
    tableDescriptionContainer.prototype.createSetColumnButtonsMethod = function(idNames, proto) {
      var setColumnButtons;
      return setColumnButtons = function(fieldSet, itemList, columnDescriptionMethod) {
        var buttonHolder, item, _i, _len;
        buttonHolder = fieldSet.find("#" + idNames.divColumnButtonHolderName);
        buttonHolder.children().remove();
        addTo(buttonHolder, proto.columnButtonCreator, {
          itemPrefix: idNames.columnButtonPrefix,
          columnList: itemList
        });
        for (_i = 0, _len = itemList.length; _i < _len; _i++) {
          item = itemList[_i];
          setClickForElement(buttonHolder, idNames.columnButtonPrefix, item, columnDescriptionMethod);
        }
        return;
      };
    };
    return tableDescriptionContainer;
  })();
  /* End Column Button Methods */
}).call(this);
