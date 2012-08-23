(function() {
  window.AttributeAction = "action";
  window.KeywordPost = "POST";
  window.aClearBothDiv = function() {
    return div({
      "class": "clearBoth"
    });
  };
  window.addTo = function(element, creator, extraParameters) {
    element.append(CoffeeKup.render(creator, extraParameters));
    return element;
  };
  window.clearHash = function(stringToClear) {
    return stringToClear.replace('#', '');
  };
  window.getFormAction = function(formName) {
    var cleanName;
    cleanName = formName.indexOf("#") === 0 ? clearHash(formName) : formName;
    return jQuery(document.getElementById(cleanName)).attr(AttributeAction);
  };
  window.setClickForElement = function(element, prefix, item, methodToCall) {
    element.find("#" + prefix + item.Id).click(function() {
      return methodToCall(item.Id);
    });
    return element;
  };
  window.setupForm = function(rules, errors, formName, createObjectMethod, errorDiv, submitCreateFormResult) {
    return jQuery(formName).validate({
      errorLabelContainer: errorDiv,
      wrapper: 'div',
      rules: rules,
      messages: errors,
      onfocusout: false,
      onkeyup: false,
      submitHandler: function(form) {
        var objectToSend;
        objectToSend = createObjectMethod();
        submitForm(objectToSend, formName, "", submitCreateFormResult);
        return;
      }
    });
  };
  window.submitForm = function(objectToSend, formName, exceptionUrl, successMethod) {
    var formAction;
    formAction = getFormAction(formName);
    submitSimple(objectToSend, formAction, successMethod);
    return;
  };
  window.submitSimple = function(objectToSend, urlToUse, successMethod) {
    return jQuery.ajax({
      type: KeywordPost,
      url: urlToUse,
      dataType: 'json',
      data: objectToSend,
      traditional: true,
      success: function(result) {
        successMethod(result);
        return;
      },
      error: function(xhr, ajaxOptions, thrownError) {
        return;
      }
    });
  };
  window.swapClassesForced = function(element, classToRemove, classToAdd) {
    if (jQuery(element).hasClass(classToRemove)) {
      jQuery(element).removeClass(classToRemove);
      void 0;
    }
    jQuery(element).addClass(classToAdd);
    return;
  };
}).call(this);
