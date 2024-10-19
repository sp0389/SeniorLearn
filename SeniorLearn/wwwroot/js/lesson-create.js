document.addEventListener('DOMContentLoaded', function () {
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

    var isCourseCheckbox = document.getElementById('isCourseCheckbox');
    var courseSelection = document.getElementById('course-selection');

    function disableHiddenFields() {
        startDateHidden.disabled = true;
        endDateHidden.disabled = true;
        startDateHidden.value = '';
        endDateHidden.value = '';
    }

    function enableHiddenFields() {
        startDateHidden.disabled = false;
        endDateHidden.disabled = false;
    }

    isCourseCheckbox.addEventListener('change', function () {
        courseSelection.style.display = this.checked ? 'block' : 'none';
    });

    singleLessonRadio.addEventListener('change', function () {
        if (singleLessonRadio.checked) {
            singleLessonDate.style.display = 'block';
            recurringLessonOptions.style.display = 'none';
        }
    });

    recurringLessonRadio.addEventListener('change', function () {
        if (recurringLessonRadio.checked) {
            recurringLessonOptions.style.display = 'block';
            singleLessonDate.style.display = 'none';
        }
    });

    dailyRecurrenceRadio.addEventListener('change', function () {
        if (dailyRecurrenceRadio.checked) {
            dailyRecurrenceSection.style.display = 'block';
            weeklyRecurrenceOptions.style.display = 'none';
        }
    });

    weeklyRecurrenceRadio.addEventListener('change', function () {
        if (weeklyRecurrenceRadio.checked) {
            dailyRecurrenceSection.style.display = 'none';
            weeklyRecurrenceOptions.style.display = 'block';
        }
    });

    occurrencesInput.addEventListener('input', function () {
        if (occurrencesInput.value > 0) {
            endDatePickerDaily.disabled = true;
            endDateHidden.value = '';
        } else {
            endDatePickerDaily.disabled = false;
        }
    });

    // Initialize flatpickr for date pickers
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
            startDateHidden.value = dateStr;
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
            endDateHidden.value = dateStr;
        }
    });

    // Flatpickr for Course Form (if needed)
    flatpickr("#courseStartDatePicker", {
        enableTime: true,
        dateFormat: "Y-m-d\\TH:i",
        altInput: true,
        altFormat: "d/m/Y H:i",
        time_24hr: true
    });

    flatpickr("#courseEndDatePicker", {
        enableTime: true,
        dateFormat: "Y-m-d\\TH:i",
        altInput: true,
        altFormat: "d/m/Y H:i",
        time_24hr: true
    });
});
