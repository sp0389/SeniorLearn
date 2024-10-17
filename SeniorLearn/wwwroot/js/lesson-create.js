document.addEventListener('DOMContentLoaded', function () {
    var toggleButton = document.getElementById('toggleButton');
    var lessonForm = document.getElementById('lessonForm');
    var courseForm = document.getElementById('courseForm');

    // Ensure lessonForm is shown and courseForm is hidden on page load
    lessonForm.style.display = 'block';
    courseForm.style.display = 'none';

    // Toggle button to switch between lesson and course forms
    toggleButton.addEventListener('click', function (e) {
        e.preventDefault();
        
        // Toggle visibility of lessonForm and courseForm
        if (lessonForm.style.display === 'block' || lessonForm.style.display === '') {
            lessonForm.style.display = 'none';
            courseForm.style.display = 'block';
            toggleButton.textContent = 'Switch to Lesson';
        } else {
            lessonForm.style.display = 'block';
            courseForm.style.display = 'none';
            toggleButton.textContent = 'Switch to Course';
        }
    });

    // Handle Lesson Recurrence Visibility
    var singleLessonRadio = document.getElementById('singleLessonRadio');
    var recurringLessonRadio = document.getElementById('recurringLessonRadio');
    var dailyRecurrenceRadio = document.getElementById('dailyRecurrenceRadio');
    var weeklyRecurrenceRadio = document.getElementById('weeklyRecurrenceRadio');

    var singleLessonDate = document.getElementById('singleLessonDate');
    var recurringLessonOptions = document.getElementById('recurringLessonOptions');
    var dailyRecurrenceSection = document.getElementById('dailyRecurrenceSection');
    var weeklyRecurrenceOptions = document.getElementById('weeklyRecurrenceOptions');

    var occurrencesInput = document.getElementById('occurrencesInput');
    var startDateHidden = document.getElementById('startDateHidden');
    var endDateHidden = document.getElementById('endDateHidden');
    var endDatePickerDaily = document.getElementById('endDatePickerDaily');

    // Helper function to disable hidden date fields
    function disableHiddenFields() {
        startDateHidden.disabled = true;
        endDateHidden.disabled = true;
        startDateHidden.value = '';  // Clear value
        endDateHidden.value = '';    // Clear value
    }

    // Helper function to enable hidden date fields
    function enableHiddenFields() {
        startDateHidden.disabled = false;
        endDateHidden.disabled = false;
    }

    // Handle Single Lesson Option
    singleLessonRadio.addEventListener('change', function () {
        if (this.checked) {
            singleLessonDate.style.display = 'block';
            recurringLessonOptions.style.display = 'none';
            disableHiddenFields();  // Disable hidden fields for single lesson
        }
    });

    // Handle Recurring Lesson Option
    recurringLessonRadio.addEventListener('change', function () {
        if (this.checked) {
            recurringLessonOptions.style.display = 'block';
            singleLessonDate.style.display = 'none';
            enableHiddenFields();  // Enable hidden fields for recurring lesson
        }
    });

    // Control recurrence options display
    dailyRecurrenceRadio.addEventListener('change', function () {
        if (this.checked) {
            dailyRecurrenceSection.style.display = 'block';
            weeklyRecurrenceOptions.style.display = 'none';
        }
    });

    weeklyRecurrenceRadio.addEventListener('change', function () {
        if (this.checked) {
            dailyRecurrenceSection.style.display = 'none';
            weeklyRecurrenceOptions.style.display = 'block';
        }
    });

    // Handle Occurrences and EndDate toggling
    occurrencesInput.addEventListener('input', function () {
        if (occurrencesInput.value > 0) {
            endDatePickerDaily.disabled = true;
            endDateHidden.value = ''; // Clear the hidden EndDate field
        } else {
            endDatePickerDaily.disabled = false;
        }
    });

    // Initialize date pickers
    flatpickr("#startDatePickerSingle", {
        enableTime: true,
        dateFormat: "Y-m-d H:i",
        altInput: true,
        altFormat: "d/m/Y H:i",
        time_24hr: true,
        defaultDate: new Date()
    });

    flatpickr("#startDatePickerDaily", {
        enableTime: true,
        dateFormat: "Y-m-d H:i",
        altInput: true,
        altFormat: "d/m/Y H:i",
        time_24hr: true,
        defaultDate: new Date(),
        onChange: function (selectedDates, dateStr) {
            startDateHidden.value = dateStr; // Store in hidden field for recurring
        }
    });

    flatpickr("#endDatePickerDaily", {
        enableTime: true,
        dateFormat: "Y-m-d H:i",
        altInput: true,
        altFormat: "d/m/Y H:i",
        time_24hr: true,
        defaultDate: new Date(),
        onChange: function (selectedDates, dateStr) {
            endDateHidden.value = dateStr; // Store in hidden field for recurring
        }
    });
});
