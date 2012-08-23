(function() {
  window.foundContainer = (function() {
    function foundContainer(thePassedInElement, itemList, callMethod) {
      var itemPrefix;
      if (!(thePassedInElement != null)) {
        throw new Error("dragAndDropList: No element was passed in.");
      }
      itemPrefix = "buttonFoundSearchItem_";
      thePassedInElement = addTo(thePassedInElement, foundContainer.prototype.searchItemCreator, {
        itemPrefix: itemPrefix,
        listData: itemList,
        callMethod: callMethod
      });
      thePassedInElement = addTo(thePassedInElement, aClearBothDiv, {});
      thePassedInElement = foundContainer.prototype.setClickEvent(thePassedInElement, itemPrefix, itemList, callMethod);
    }
    foundContainer.prototype.setClickEvent = function(element, prefix, itemList, methodToCall) {
      var item, _i, _len;
      for (_i = 0, _len = itemList.length; _i < _len; _i++) {
        item = itemList[_i];
        element = setClickForElement(element, prefix, item, methodToCall);
      }
      return element;
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
            "class": "searchItemLabel"
          }, function() {
            return span("" + item.TableName);
          });
          div({
            "class": "searchItemSchema"
          }, function() {
            return span("" + item.SchemaName);
          });
          return div({
            "class": "searchItemDatabase"
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
