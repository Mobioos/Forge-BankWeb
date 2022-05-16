function initWebPageWithData() {
  const path = '../data/default.json';
  fetch(path)
    .then((response) => response.json())
    .then((data) =>
      populateWebpageWithData(
        data.name,
        data.description,
        data.phoneNumber,
        data.mail
      )
    );
}

function populateWebpageWithData(
  bankName,
  bankDescription,
  bankPhone,
  bankMail
) {
  $('#bankNameContainer').html(bankName);
  $('#bankDescriptionContainer').html(`<em>${bankDescription}</em>`);
  $('#phoneNumberContainer').html(bankPhone);
  $('#mailContainer').html(bankMail);
}
