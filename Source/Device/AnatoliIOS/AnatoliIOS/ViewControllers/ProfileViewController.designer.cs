// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AnatoliIOS.ViewControllers
{
	[Register ("ProfileViewController")]
	partial class ProfileViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField addressTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField emailTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField lastNameTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIPickerView level1Picker { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIPickerView level2Picker { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIPickerView level3Picker { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIPickerView level4Picker { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField nameTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton saveButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (addressTextField != null) {
				addressTextField.Dispose ();
				addressTextField = null;
			}
			if (emailTextField != null) {
				emailTextField.Dispose ();
				emailTextField = null;
			}
			if (lastNameTextField != null) {
				lastNameTextField.Dispose ();
				lastNameTextField = null;
			}
			if (level1Picker != null) {
				level1Picker.Dispose ();
				level1Picker = null;
			}
			if (level2Picker != null) {
				level2Picker.Dispose ();
				level2Picker = null;
			}
			if (level3Picker != null) {
				level3Picker.Dispose ();
				level3Picker = null;
			}
			if (level4Picker != null) {
				level4Picker.Dispose ();
				level4Picker = null;
			}
			if (nameTextField != null) {
				nameTextField.Dispose ();
				nameTextField = null;
			}
			if (saveButton != null) {
				saveButton.Dispose ();
				saveButton = null;
			}
		}
	}
}
