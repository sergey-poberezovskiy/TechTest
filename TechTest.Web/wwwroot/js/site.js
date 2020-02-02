const baseUri = 'api/CheckWriter';

function calculate() {
	if ($("form").valid()) {
		const txtAmountInWords = $("#AmountInWords");
		const txtCheckAmount = $("#CheckAmount");
		const chkInUpper = $("#InUpper");
		const divError = $("#divError");

		divError.innerHTML = "";
		var uri = baseUri + "/" + txtCheckAmount.val();
		if (chkInUpper.prop("checked"))
			uri += "/true";

		fetch(uri, {
			headers: {
				"Accept": "application/json",
				'Content-Type': 'application/json'
			}
		})
			.then(response => response.json())
			.then(data => txtAmountInWords.val(data))
			.catch(error => divError.html("Unable to get response from API:<br/>" + error));
	}
}
