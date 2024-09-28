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
                var userId = user.uid;
                // Cek apakah ada username di Firestore (scores collection)
                firebase.firestore().collection('scores').doc(userId).get()
                .then(function(doc) {
                    if (doc.exists && doc.data().igusername) {
                        // Kirim username yang ditemukan ke Unity
                        Module.SendMessage('FirebaseController', 'OnGoogleSignInSuccess', doc.data().igusername);
                    } else {
                        // Jika tidak ada username, kirim string kosong untuk meminta input dari pemain
                        Module.SendMessage('FirebaseController', 'OnGoogleSignInSuccess', '');
                    }
                })
                .catch(function(error) {
                    console.error('Error getting user data:', error);
                    Module.SendMessage('FirebaseController', 'OnGoogleSignInFail', error.message);
                });
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
            }, { merge: true })
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
                        username: doc.data().igusername,
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
    },

    SaveBattery: function(battery) {
        if (typeof firebase === 'undefined') {
            console.error('Firebase is not defined.');
            return;
        }

        var user = firebase.auth().currentUser;
        if (user) {
            var userId = user.uid;
            firebase.firestore().collection('scores').doc(userId).set({
                battery: battery // Simpan nilai battery
            }, { merge: true }) // Use merge to avoid overwriting other fields
            .then(function() {
                console.log('Battery saved successfully');
            })
            .catch(function(error) {
                console.error('Error saving battery:', error);
            });
        } else {
            console.log('No user is signed in.');
        }
    },

    GetBattery: function() {
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
                    var battery = doc.data().battery || 0; // Set default to 0 if battery is not found
                    // Kirim nilai battery ke Unity
                    Module.SendMessage('FirebaseController', 'OnReceiveBattery', battery.toString());
                } else {
                    Module.SendMessage('FirebaseController', 'OnReceiveBattery', '0');
                }
            })
            .catch(function(error) {
                console.error('Error getting battery:', error);
                Module.SendMessage('FirebaseController', 'OnReceiveBattery', 'Error');
            });
        } else {
            console.log('No user is signed in.');
            Module.SendMessage('FirebaseController', 'OnReceiveBattery', 'No user');
        }
    },

    SaveUsername: function(username) {
        if (typeof firebase === 'undefined') {
            console.error('Firebase is not defined.');
            return;
        }

        console.log("Saving username:", username, "Type:", typeof username);

        // Konversi string ke string (untuk menghindari encoding yang salah)
        var userString = UTF8ToString(username); 

        var user = firebase.auth().currentUser;
        if (user) {
            var userId = user.uid;
            firebase.firestore().collection('scores').doc(userId).set({
                igusername: userString
            }, { merge: true })
            .then(function() {
                console.log('Username saved successfully');
            })
            .catch(function(error) {
                console.error('Error saving username:', error);
            });
        } else {
            console.log('No user is signed in.');
        }
    }
});