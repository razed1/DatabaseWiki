(function() {
  window.createErrorMessageFromResult = function(result) {
    var errorMessage, item, _fn, _i, _len, _ref;
    errorMessage = "";
    if ((result.Messages != null) && result.Messages.length > 0) {
      _ref = result.Messages;
      _fn = function(item) {
        errorMessage += item.concat("<br/>");
        return;
      };
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        item = _ref[_i];
        _fn(item);
      }
    }
    return errorMessage;
  };
  window.resetErrorStyle = function(errorDiv) {
    return swapClassesForced(jQuery(errorDiv), 'ui-state-highlight', 'ui-state-error');
  };
  window.updateErrorDivFromResult = function(result, controlToUseForError, formToUse, errorDiv) {
    var errorList, errorMessage, validator;
    validator = jQuery(formToUse).validate();
    errorList = Object();
    errorMessage = createErrorMessageFromResult(result);
    errorList[controlToUseForError] = errorMessage;
    validator.showErrors(errorList);
    return setErrorDivPresentationBasedOnResultSuccess(result, errorDiv);
  };
  window.setErrorDivPresentationBasedOnResultSuccess = function(result, errorDiv) {
    if (result.Success) {
      swapClassesForced(jQuery(errorDiv), 'ui-state-error', 'ui-state-highlight');
      return;
    } else {
      swapClassesForced(jQuery(errorDiv), 'ui-state-highlight', 'ui-state-error');
      return;
    }
  };
}).call(this);
