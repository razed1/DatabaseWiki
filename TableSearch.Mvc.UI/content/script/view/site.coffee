window.AttributeAction = "action"
window.KeywordPost = "POST"


window.aClearBothDiv = ->
  div class:"clearBoth"


window.addTo = (element, creator, extraParameters) ->
  element.append(CoffeeKup.render(creator, extraParameters))
  element


window.clearHash = (stringToClear) ->
  stringToClear.replace('#', '')


window.getFormAction = (formName) ->
  cleanName = if formName.indexOf("#") is 0 then clearHash(formName) else formName
  jQuery(document.getElementById(cleanName)).attr(AttributeAction)


window.setClickForElement = (element, prefix, item, methodToCall) ->
    element.find("##{prefix}#{item.Id}").click -> methodToCall(item.Id)
    element


window.setupForm = (rules, errors, formName, createObjectMethod, errorDiv, submitCreateFormResult) ->

  jQuery(formName).validate({
  errorLabelContainer: errorDiv,
  wrapper: 'div',
  rules: rules,
  messages: errors,
  onfocusout: false,
  onkeyup: false,
  submitHandler: (form) ->
    objectToSend = createObjectMethod()
    submitForm(objectToSend, formName, "", submitCreateFormResult)
    undefined
  });

window.submitForm = (objectToSend, formName, exceptionUrl, successMethod) ->
  formAction = getFormAction(formName);
  submitSimple(objectToSend, formAction, successMethod)
  undefined


window.submitSimple = (objectToSend, urlToUse, successMethod) ->
  jQuery.ajax({
  type: KeywordPost,
  url: urlToUse,
  dataType: 'json',
  data: objectToSend,
  traditional: true,
  success: (result) ->
    successMethod(result)
    undefined
  ,
  error: (xhr, ajaxOptions, thrownError) ->
    undefined
  });


window.swapClassesForced = (element, classToRemove, classToAdd)  ->
  if (jQuery(element).hasClass(classToRemove))
    jQuery(element).removeClass(classToRemove)
    undefined

  jQuery(element).addClass(classToAdd)
  undefined
