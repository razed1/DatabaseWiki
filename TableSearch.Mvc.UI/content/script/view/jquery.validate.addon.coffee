window.createErrorMessageFromResult = (result) ->
	errorMessage = ""

	if result.Messages? and result.Messages.length > 0
		for item in result.Messages
			do (item) ->
				errorMessage += item.concat("<br/>")
				undefined

	errorMessage


window.resetErrorStyle = (errorDiv) ->
    swapClassesForced(jQuery(errorDiv), 'ui-state-highlight', 'ui-state-error')


window.updateErrorDivFromResult = (result, controlToUseForError, formToUse, errorDiv) ->
	validator = jQuery(formToUse).validate()
	errorList = Object()
	errorMessage = createErrorMessageFromResult(result)

	errorList[controlToUseForError] = errorMessage
	validator.showErrors(errorList)
	setErrorDivPresentationBasedOnResultSuccess(result, errorDiv)


window.setErrorDivPresentationBasedOnResultSuccess = (result, errorDiv) ->
	if result.Success
		swapClassesForced(jQuery(errorDiv), 'ui-state-error', 'ui-state-highlight')
		undefined
	else
		swapClassesForced(jQuery(errorDiv), 'ui-state-highlight', 'ui-state-error')
		undefined