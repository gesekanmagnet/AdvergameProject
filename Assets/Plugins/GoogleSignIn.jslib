mergeInto(LibraryManager.library, {
    GoogleSignIn: function() {
        if (typeof firebase === 'undefined') {
            console.error('Firebase is not defined.');
            return;
        }

        var provider = new firebase.auth.GoogleAuthProvider();
        firebase.auth().signInWithPopup(provider).then(function(result) {
            var user = result.user;
            // Kirim pesan ke Unity
            Module.SendMessage('FirebaseController', 'OnGoogleSignInSuccess', user.displayName);
        }).catch(function(error) {
            // Kirim pesan kesalahan ke Unity
            Module.SendMessage('FirebaseController', 'OnGoogleSignInFail', error.message);
        });
    }
});
