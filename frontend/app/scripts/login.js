const firebaseConfig = {
  apiKey: 'AIzaSyDhJhq-aUbAm90lYp43TVnb7b6jCprAbDw',
  authDomain: 'mobioos-bank-web-1d0d8.firebaseapp.com',
  projectId: 'mobioos-bank-web-1d0d8',
  storageBucket: 'mobioos-bank-web-1d0d8.appspot.com',
  messagingSenderId: '445080367244',
  appId: '1:445080367244:web:7114ebcfb481cd10038ba9',
};

// Initialize Firebase
firebase.initializeApp(firebaseConfig);

const DEFAULT_TOKEN = 'NO_TOKEN';
const DEFAULT_PROVIDER = 'NO_PROVIDER';

let token = DEFAULT_TOKEN;
let oauthProvider = DEFAULT_PROVIDER;

window.addEventListener('load', (_) => checkLoggedStatus());

function appearsAsLogged() {
  $('#logout-button-container').show();
  $('#logged-tag').show();
  $('#not-logged-tag').hide();
  $('#login-buttons-container').hide();
  $('#deposit-button').prop('disabled', false);
  $('#withdraw-button').prop('disabled', false);
  $('#limit-checkbox').prop('disabled', false);
  $('input:radio[name="cards"]').prop('disabled', false);
}

function appearsNotLogged() {
  $('#login-buttons-container').show();
  $('#not-logged-tag').show();
  $('#logged-tag').hide();
  $('#logout-button-container').hide();
  $('#deposit-button').prop('disabled', true);
  $('#withdraw-button').prop('disabled', true);
  $('#limit-checkbox').prop('disabled', true);
  $('input:radio[name="cards"]').prop('disabled', true);
}

function checkLoggedStatus() {
  const user = firebase.auth().currentUser;
  if (user !== null) {
    user
      .getIdToken()
      .then((value) => {
        token = value;
        appearsAsLogged();
      })
      .catch((_) => appearsNotLogged());
  } else {
    appearsNotLogged();
  }
}

$('#logout-button').click((_) => {
  firebase
    .auth()
    .signOut()
    .finally(() => {
      token = DEFAULT_TOKEN;
      oauthProvider = DEFAULT_PROVIDER;
      appearsNotLogged();
    });
});

//////////////////////////////////////
////////////// GOOGLE ////////////////
//////////////////////////////////////

function googleLogin() {
  const provider = new firebase.auth.GoogleAuthProvider();
  const auth = firebase.auth();
  auth.useDeviceLanguage();
  auth
    .signInWithPopup(provider)
    .then(onGoogleSuccessLogin)
    .catch(onGoogleErrorLogin);
}

function onGoogleSuccessLogin(result) {
  token = result.credential.accessToken;
  oauthProvider = 'Google';
  appearsAsLogged();
}

function onGoogleErrorLogin(error) {
  console.error('Google error ', error);
  appearsNotLogged();
}

$('#google-login-button').click((_) => googleLogin());

//////////////////////////////////////
/////////////// GITHUB ///////////////
//////////////////////////////////////
function githubLogin() {
  const provider = new firebase.auth.GithubAuthProvider();
  const auth = firebase.auth();
  auth.useDeviceLanguage();
  auth
    .signInWithPopup(provider)
    .then(onGithubSuccessLogin)
    .catch(onGithubErrorLogin);
}

function onGithubSuccessLogin(result) {
  console.log(result);
  token = result.credential.accessToken;
  oauthProvider = 'Github';
  appearsAsLogged();
}

function onGithubErrorLogin(error) {
  console.error('Github error ', error);
  appearsNotLogged();
}

$('#github-login-button').click((_) => githubLogin());

