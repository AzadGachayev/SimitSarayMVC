/**
 * Reservation form
 */

"use strict";

$(document).on('opening', '#modal', function() {

	var reservation = (function() {

		// Variables
		var form =			$(document).find('#reservation__form');
		var formName = 		form.find('#reservation__name');
		var formPhone = 	form.find('#reservation__phone');
		var formEmail = 	form.find('#reservation__email');
		var formPeople = 	form.find('#reservation__people');
		var formDate = 		form.find('#reservation__date');
		var formTime = 		form.find('#reservation__time');
		var formSubmit = 	form.find('[type="submit"]');
		var formActionUrl = 'assets/plugins/reservation/reservation.php';

		// Methods
		function getCurrentDate() {
			var today = new Date();
			var dd = today.getDate();
			var mm = today.getMonth() + 1;
			var yyyy = today.getFullYear();

			if ( dd < 10 ) {
				dd = '0' + dd;
			}
			if ( mm < 10 ) {
				mm = '0' + mm;
			}

			return yyyy + '-' + mm + '-' + dd;
		}
		function setCurrentDate() {
			var today = getCurrentDate();
			formDate.attr('value', today);
		}
		function submitForm($this) {

			$.ajax({
				url: formActionUrl,
				type: 'POST',
				data: $this.serialize(),
				dataType: 'json',
				beforeSend: function (XMLHttpRequest) {

					// Disable submit button
					formSubmit.prop('disabled', true);

					// Clear error messages
					form.find('.is-invalid').removeClass('is-invalid');
					form.find('.invalid-feedback').html('');

				},
				success: function( json, textStatus ) {

					// Enable submit button
					formSubmit.prop('disabled', false);

					function showError(elem, message) {
						elem.addClass('is-invalid');
						elem.next('.invalid-feedback').html(message);
					}

					if( json.error ) {

						// Proceed error messages
						if ( json.error.name ) {
							showError(formName, json.error.name);
						}
						if ( json.error.phone ) {
							showError(formPhone, json.error.phone);
						}
						if ( json.error.email ) {
							showError(formEmail, json.error.email);
						}
						if ( json.error.people ) {
							showError(formPeople, json.error.people);
						}
						if ( json.error.date ) {
							showError(formDate, json.error.date);
						}
						if ( json.error.time ) {
							showError(formTime, json.error.time);
						}
					}

					// Proceed success message
					if( json.success ) {

						// Show alert message
						$(document).trigger('entreys.alert.show', ['success', json.success]);

						// Reset form fields
						form[0].reset();
					}
				}
			});

		}

		// Set current date
		setCurrentDate();

		// Process form
		$(document).find(form).submit(function(e) {
			e.preventDefault();

			submitForm( $(this) );
		});

	})();

});