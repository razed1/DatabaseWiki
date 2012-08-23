(function() {
  window.aClearBothDiv = function() {
    return div({
      "class": "clearBoth"
    });
  };
  window.addTo = function(element, creator, extraParameters) {
    element.append(CoffeeKup.render(creator, extraParameters));
    return element;
  };
  window.setClickForElement = function(element, prefix, item, methodToCall) {
    element.find("#" + prefix + item.Id).click(function() {
      return methodToCall(item.Id);
    });
    return element;
  };
}).call(this);
