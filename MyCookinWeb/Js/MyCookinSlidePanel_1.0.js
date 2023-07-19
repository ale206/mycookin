﻿$(function () {
    $('ul.sezioni li').hover(function () {
        $(this).find('img').animate({ top: '182px' }, { queue: false, duration: 500 });
    }, function () {
        $(this).find('img').animate({ top: '0px' }, { queue: false, duration: 500 });
    });

    //		    $('ul.sezioni2 li').hover(function () {
    //		        $(this).find('img').animate({ left: '300px' }, { queue: false, duration: 500 });
    //		    }, function () {
    //		        $(this).find('img').animate({ left: '0px' }, { queue: false, duration: 500 });
    //		    });
});

$(document).ready(function () {
    var currentPosition = 0;
    var slideWidth = 560;
    var slides = $('.slide');
    var numberOfSlides = slides.length;

    // Remove scrollbar in JS
    $('#slidesContainer').css('overflow', 'hidden');
    // Wrap all .slides with #slideInner div
    slides
        .wrapAll('<div id="slideInner"></div>')
    // Float left to display horizontally, readjust .slides width

        .css({
            'float': 'left',
            'width': slideWidth
        });

    // Set #slideInner width equal to total width of all slides
    $('#slideInner').css('width', slideWidth * numberOfSlides);
    // Insert controls in the DOM
    $('#slideshow')
    .prepend('<span class="control" id="leftControl"></span>')
    .append('<span class="control" id="rightControl"></span>');

    // Hide left arrow control on first load
    manageControls(currentPosition);

    // Create event listeners for .controls clicks
    $('.control')
    .bind('click', function () {
        // Determine new position
        currentPosition = ($(this).attr('id') == 'rightControl') ? currentPosition + 1 : currentPosition - 1;

        // Hide / show controls
        manageControls(currentPosition);

        // Move slideInner using margin-left
        $('#slideInner').animate({
            'marginLeft': slideWidth * (-currentPosition)
        });
    });

    // manageControls: Hides and Shows controls depending on currentPosition

    function manageControls(position) {
        // Hide left arrow if position is first slide
        if (position == 0) { $('#leftControl').hide() } else { $('#leftControl').show() }

        // Hide right arrow if position is last slide
        if (position == numberOfSlides - 1) { $('#rightControl').hide() } else { $('#rightControl').show() }
    }
});