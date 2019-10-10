/*/**
 * Theme JS
 */

'use strict';


/*** Reservation Modal ***/

(function() {

	// Variables
	var modal = $('#modal');

	// Methods
	function init() {
		modal.iziModal({
			background: '#343a40',
			openFullscreen: true,
			focusInput: false,
			restoreDefaultContent: true
		});
	}

	// Events
	init();

})();


/*** Pages Animation (Animsition Plugin) ***/

(function() {

	// Variables
	var animsition = $(".animsition");

	// Methods
	function init() {
		animsition.animsition({
			inClass: 'fade-in',
    		outClass: 'fade-out',
		});
	}

	// Events
	init();

})();


/*** Navbar ***/

(function() {

	// Variables
	var navbar = $('.navbar');
	var navbarCollapse = $('.navbar-collapse');
	var navbarLink = $('.nav-link');

	// Methods
	function makeNavbarDark() {
		navbar.removeClass('navbar-light').addClass('navbar-dark');
	}
	function makeNavbarLight() {
		navbar.removeClass('navbar-dark').addClass('navbar-light');
	}
	function toggleNavbarClass() {
		var scrollTop = $(window).scrollTop();

		if ( scrollTop > 5 ) {
			makeNavbarDark();
		} else {
			makeNavbarLight();
		}
	}
	// Events

	// Toggle navbar on page load if needed
	toggleNavbarClass();

	// Toggle navbar on scroll
	$(window).on('scroll', function() {
		toggleNavbarClass();
	});

	// Toggle navbar class on collapse
	navbarCollapse.on({
		'show.bs.collapse': function() {
			makeNavbarDark();
		},
		'hidden.bs.collapse': function() {
			var scrollTop = $(window).scrollTop();

			if (scrollTop == 0) { 
				makeNavbarLight();
			}
		}
	});

	// Close collapsed navbar on click
	navbarLink.on('click', function() {
		
		if ( $(this).filter('[href^="#section_"]').length ) {
			navbarCollapse.collapse('hide');
		}
	});

	// Navbar active links fix
	$(window).on('activate.bs.scrollspy', function() {
		navbarLink.filter('.active').focus();
	});

})();


/*** Newsletter ***/

(function() {

	// Variables
	var $form = $('#mc-embedded-subscribe-form');

	// Proceed form
	function register() {

		$.ajax({
			type: $form.attr('method'),
			url: $form.attr('action'),
			data: $form.serialize(),
			cache: false,
			dataType: 'json',
			contentType: "application/json; charset=utf-8",
			error: function(err) {
				$(document).trigger('entreys.alert.show', ['danger', 'Could not connect to the registration server. Please try again later.']);
			},
			success: function(data) {

				if (data.result != "success") {

					// Trim error message
					var msg = data.msg;
						msg = msg.substr(3);
						
					$(document).trigger('entreys.alert.show', ['danger', msg]);
				} else {

					// Show a confirmation
					$(document).trigger('entreys.alert.show', ['success', data.msg]);
					
					// Reset a form
					$form[0].reset();
				}
			}
		});
	}

	// Events
	$form.on('submit', function(e) {
		register();

		e.preventDefault();
	});

})();


/*** Gallery Carousel ***/

var gallery = (function() {

    // Variables
    var container = $('.section_gallery_carousel');

    // Methods
    function init() {
        container.flickity({
            cellAlign: 'left',
            initialIndex: 0,
            imagesLoaded: true,
            wrapAround: true,
            pageDots: false

        });
    }

    // Events
    if ( container.length ) {
        init();
    }

})();


/*** Gallery grid (Interior Section) ***/

var interior = (function() {

    // Variables
    var gallery = $('.section_interior_grid');
    var galleryItemSelector = '.section_interior_grid_item';
    var grid;

    // Methods
    function init() {
        grid = gallery.isotope({
            itemSelector: galleryItemSelector
        });
        grid.imagesLoaded().progress( function() {
            grid.isotope('layout');
        });
    };

    // Events
    if ( gallery.length ) {
        init();
    }

})();


/*** Testimonials Carousel ***/

var testimonials = (function() {

    // Variables
    var container = $('.section_testimonials_carousel');

    // Methods
    function init() {
        container.flickity({
            cellAlign: 'left',
            initialIndex: 1,
            imagesLoaded: true,
            wrapAround: true,
            pageDots: false

        });
    }

    // Events
    if ( container.length ) {
        init();
    }

})();
