# DropboxApi-Sharing

This is a simple tutorial that uses Dropbox Api to create folders and share them with specific email addresses.

The only restriction is that it shares 40 folders/day

To make it work:
1- Create an app from https://www.dropbox.com/developers then generate oauth key.
2- Create a CSV file with 2 columns, the first is the Folder Name and the second is the email to share the folder with.
3- Open the config file and replace the following:
	a- DropboxOauthToken
	b- DropboxFolderPath
	c- SharingMessage