(function() {
  window.tableDescriptionContainer = (function() {
    function tableDescriptionContainer(thePassedInElement, columnDescriptionMethod) {
      var proto;
      if (!(thePassedInElement != null)) {
        throw new Error("dragAndDropList: No element was passed in.");
      }
      proto = tableDescriptionContainer.prototype;
      thePassedInElement = addTo(thePassedInElement, proto.tableDescriptionContainerCreator, {});
      proto.addUpdateDescriptionMethodTo(thePassedInElement, proto.setColumnButtons, columnDescriptionMethod);
      thePassedInElement;
    }
    /* Table Description Area Methods */
    tableDescriptionContainer.prototype.addUpdateDescriptionMethodTo = function(element, createButtons, columnDescriptionMethod) {
      return element[0].updateDescription = function(result) {
        var fieldSet;
        fieldSet = jQuery(element).find(".fieldsetTableDescriptionText");
        fieldSet.find(".divDescriptionText").text(result.Description);
        fieldSet.find("#legend").text(result.TableName);
        createButtons(fieldSet, result.ColumnList, columnDescriptionMethod);
        fieldSet.show();
        return;
      };
    };
    tableDescriptionContainer.prototype.tableDescriptionContainerCreator = function() {
      return fieldset({
        "class": "fieldsetTableDescriptionText",
        style: "display:none;"
      }, function() {
        legend({
          id: "legend"
        }, function() {
          return "";
        });
        div({
          "class": "divDescriptionText"
        }, function() {
          return "";
        });
        return div({
          id: "divColumnButtonHolder"
        }, function() {
          return "";
        });
      });
    };
    /* Column Button Methods */
    tableDescriptionContainer.prototype.setColumnButtons = function(fieldSet, itemList, columnDescriptionMethod) {
      var buttonHolder, item, prefix, proto, _i, _len;
      prefix = "buttonColumnItem_";
      proto = tableDescriptionContainer.prototype;
      buttonHolder = fieldSet.find("#divColumnButtonHolder");
      buttonHolder.children().remove();
      addTo(buttonHolder, proto.columnButtonCreator, {
        itemPrefix: prefix,
        columnList: itemList
      });
      for (_i = 0, _len = itemList.length; _i < _len; _i++) {
        item = itemList[_i];
        setClickForElement(buttonHolder, prefix, item, columnDescriptionMethod);
      }
      return;
    };
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
    return tableDescriptionContainer;
  })();
}).call(this);
