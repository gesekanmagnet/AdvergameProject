mergeInto(LibraryManager.library, {
    GoogleSignIn: function() {
        if (typeof firebase === 'undefined') {
            console.error('Firebase is not defined.');
            return;
        }

        var provider = new firebase.auth.GoogleAuthProvider();
        firebase.auth().signInWithPopup(provider).then(function(result) {
            var user = result.user;
            if (user) {
                const userDoc = db.collection("users").doc(user.uid);
                // Kirim nama pengguna ke Unity
                Module.SendMessage('FirebaseController', 'OnGoogleSignInSuccess', user.displayName || 'Unknown User');
            }
        }).catch(function(error) {
            // Kirim pesan kesalahan ke Unity
            Module.SendMessage('FirebaseController', 'OnGoogleSignInFail', error.message);
        });
    },

    SaveScore: function(score) {
        if (typeof firebase === 'undefined') {
            console.error('Firebase is not defined.');
            return;
        }

        var user = firebase.auth().currentUser;
        if (user) {
            var userId = user.uid; // ID pengguna saat ini
            firebase.firestore().collection('scores').doc(userId).set({
                score: score, // Menyimpan skor
                username: user.displayName || 'Unknown User' // Menyimpan nama pengguna
            })
            .then(function() {
                console.log('Score and username saved successfully');
            })
            .catch(function(error) {
                console.error('Error saving score and username:', error);
            });
        } else {
            console.log('No user is signed in.');
        }
    },

    GetScore: function() {
        if (typeof firebase === 'undefined') {
            console.error('Firebase is not defined.');
            return;
        }

        var user = firebase.auth().currentUser;
        if (user) {
            var userId = user.uid;
            firebase.firestore().collection('scores').doc(userId).get()
            .then(function(doc) {
                if (doc.exists) {
                    // Ambil nilai skor dari dokumen
                    var score = doc.data().score;
                    // Kirim skor ke Unity
                    Module.SendMessage('FirebaseController', 'OnReceiveScore', score.toString());
                } else {
                    Module.SendMessage('FirebaseController', 'OnReceiveScore', 'No score found');
                }
            })
            .catch(function(error) {
                console.error('Error getting score:', error);
                Module.SendMessage('FirebaseController', 'OnReceiveScore', 'Error');
            });
        } else {
            console.log('No user is signed in.');
            Module.SendMessage('FirebaseController', 'OnReceiveScore', 'No user');
        }
    },

    GetTopScores: function() {
        if (typeof firebase === 'undefined') {
            console.error('Firebase is not defined.');
            return;
        }

        firebase.firestore().collection('scores')
            .orderBy('score', 'desc')
            .limit(10)
            .get()
            .then(function(querySnapshot) {
                var leaderboardData = [];
                querySnapshot.forEach(function(doc) {
                    leaderboardData.push({
                        username: doc.data().username,
                        score: doc.data().score
                    });
                });
                // Kirim data leaderboard ke Unity dalam bentuk JSON
                Module.SendMessage('FirebaseController', 'OnReceiveTopScores', JSON.stringify(leaderboardData));
            })
            .catch(function(error) {
                console.error('Error getting leaderboard:', error);
                Module.SendMessage('FirebaseController', 'OnReceiveTopScores', 'Error');
            });
    }
});