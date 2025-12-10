using UnityEngine;
using UnityEngine.UI;

public class LimitInputField : MonoBehaviour
{
	public enum ContentType
	{
		Standard,
		IntegerNumber,
		DecimalNumber,
		PositiveInteger,//(含0)
		PositiveDecimal,//(含0)

		Custorm,
	}

	public ContentType contentType;


	private InputField inputField;




	private char ValidateCallBack(string text, int charIndex, char ch)
	{
		switch (contentType)
		{
			case ContentType.Standard:
				return ch;
			case ContentType.IntegerNumber:
				break;
			case ContentType.DecimalNumber:
				break;
			case ContentType.PositiveInteger:
				break;
			case ContentType.PositiveDecimal:
				break;
			case ContentType.Custorm:
				break;
			default:
				break;
		}

		return ch;
	}


}