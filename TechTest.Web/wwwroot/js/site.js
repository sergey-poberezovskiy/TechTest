const baseUri = 'api/CheckWriter';

function calculate() {
	if ($("form").valid()) {
		const txtAmountInWords = document.getElementById("AmountInWords");
		const txtCheckAmount = document.getElementById("CheckAmount");
		const chkInUpper = document.getElementById("InUpper");
		const divError = document.getElementById("divError");

		divError.innerHTML = "";
		var uri = baseUri + "/" + txtCheckAmount.value;
		if (chkInUpper.checked)
			uri += "/true";

		fetch(uri, {
			headers: {
				"Accept": "application/json",
				'Content-Type': 'application/json'
			}
		})
			.then(response => response.json())
			.then(data => txtAmountInWords.value = data)
			.catch(error => divError.innerHTML = "Unable to get response from API:<br/>" + error);
	}
}