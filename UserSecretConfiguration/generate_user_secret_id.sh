user_secret_id=$(uuidgen)
dot_microsoft_dir=.microsoft/usersecrets/
cd $HOME
mkdir -p $dot_microsoft_dir
cd $dot_microsoft_dir
mkdir $user_secret_id
touch $user_secret_id/secrets.json
echo '{"port": "8000", "host": "locahost", "env": "Development"}' > $user_secret_id/secrets.json
echo 'Please, copy & paste the following guid to AssemblyInfo.cs -> ' $user_secret_id
