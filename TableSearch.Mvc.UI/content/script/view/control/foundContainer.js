(function() {
  window.foundContainer = (function() {
    function foundContainer(thePassedInElement, callMethod) {
      var idBag, proto;
      if (!(thePassedInElement != null)) {
        throw new Error("dragAndDropList: No element was passed in.");
      }
      idBag = {
        buttonFoundPrefix: "buttonFoundSearchItem_",
        searchItemDatabaseName: "searchItemDatabase",
        searchItemLabelName: "searchItemLabel",
        searchItemSchemaName: "searchItemSchema"
      };
      proto = foundContainer.prototype;
      proto.addTableButtonsMethodTo(thePassedInElement, proto.createSetTableButtonsMethod(idBag), callMethod, idBag);
    }
    foundContainer.prototype.setClickEvent = function(element, prefix, itemList, methodToCall) {
      var item, _i, _len;
      for (_i = 0, _len = itemList.length; _i < _len; _i++) {
        item = itemList[_i];
        element = setClickForElement(element, prefix, item, methodToCall);
      }
      return element;
    };
    foundContainer.prototype.addTableButtonsMethodTo = function(element, createButtons, callMethod, idNames) {
      return element[0].updateResults = function(result) {
        var proto;
        proto = foundContainer.prototype;
        createButtons(element, result.Value);
        addTo(element, aClearBothDiv, {});
        proto.setClickEvent(element, idNames.buttonFoundPrefix, result.Value, callMethod);
        element.show();
        return;
      };
    };
    foundContainer.prototype.createSetTableButtonsMethod = function(idNames) {
      var setTableButtons;
      return setTableButtons = function(container, itemList) {
        var proto;
        proto = foundContainer.prototype;
        container.children().remove();
        addTo(container, proto.searchItemCreator, {
          idNames: idNames,
          itemPrefix: idNames.buttonFoundPrefix,
          listData: itemList
        });
        return;
      };
    };
    foundContainer.prototype.searchItemCreator = function() {
      var item, _i, _len, _ref, _results;
      _ref = this.listData;
      _results = [];
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        _results.push(button({
          id: "" + this.itemPrefix + item.Id,
          "class": "searchItem floatLeft"
        }, function() {
          div({
            id: this.idNames.searchItemLabelName,
            "class": this.idNames.searchItemLabelName
          }, function() {
            return span("" + item.TableName);
          });
          div({
            id: this.idNames.searchItemSchemaName,
            "class": this.idNames.searchItemSchemaName
          }, function() {
            return span("" + item.SchemaName);
          });
          return div({
            id: this.idNames.searchItemDatabaseName,
            "class": this.idNames.searchItemDatabaseName
          }, function() {
            return span("" + item.DatabaseName);
          });
        }));
      }
      return _results;
    };
    return foundContainer;
  })();
}).call(this);
