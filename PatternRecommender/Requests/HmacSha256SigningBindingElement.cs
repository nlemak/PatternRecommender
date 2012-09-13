﻿//-----------------------------------------------------------------------
// <copyright file="HmacSha256SigningBindingElement.cs">
//     Copyright (c) Natasha Lemak. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DotNetOpenAuth.OAuth.ChannelElements {
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Diagnostics.Contracts;
	using System.Security.Cryptography;
	using System.Text;
	using DotNetOpenAuth.Messaging;

	/// <summary>
	/// A binding element that signs outgoing messages and verifies the signature on incoming messages.
	/// </summary>
	public class HmacSha256SigningBindingElement : SigningBindingElementBase {
		/// <summary>
		/// Base constructor
		/// </summary>
		public HmacSha256SigningBindingElement()
			: base("HMAC-SHA256") {
		}

		/// <summary>
		/// Calculates a signature for a given message.
		/// </summary>
		/// <param name="message">The message to sign.</param>
		/// <returns>The signature for the message.</returns>
		/// <remarks>
		/// This method signs the message per OAuth 1.0 section 9.2.
		/// </remarks>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "False positive.")]
		protected override string GetSignature(ITamperResistantOAuthMessage message) {
			string key = GetConsumerAndTokenSecretString(message);
			using (HashAlgorithm hasher = new HMACSHA256(Encoding.ASCII.GetBytes(key))) {
				string baseString = ConstructSignatureBaseString(message, this.Channel.MessageDescriptions.GetAccessor(message));
				byte[] digest = hasher.ComputeHash(Encoding.ASCII.GetBytes(baseString));
				return Convert.ToBase64String(digest);
			}
		}

		/// <summary>
		/// Clones this instance.
		/// </summary>
		/// <returns>A new instance of the binding element.</returns>
		protected override ITamperProtectionChannelBindingElement Clone() {
			return new HmacSha256SigningBindingElement();
		}
	}
}