using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    public RectTransform canvasRectTransform; // RectTransform của Canvas
    public RectTransform referenceRectTransform; // RectTransform của đối tượng tham chiếu để tính tỉ lệ

    // Tỉ lệ giữa kích thước của đối tượng tham chiếu và kích thước màn hình
    private float scale_reference_Ratio;
    private float scaleYRatio;

    float posXRatio;
    float posYRatio;

    void Start()
    {
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
        referenceRectTransform = GetComponent<RectTransform>();

        // Tính toán tỉ lệ giữa kích thước của đối tượng tham chiếu và kích thước màn hình
        scaleYRatio = referenceRectTransform.rect.height / canvasRectTransform.rect.height;
        scale_reference_Ratio = referenceRectTransform.rect.width / referenceRectTransform.rect.height;

        // Tính toán tỷ lệ giữa tọa độ của đối tượng tham chiếu và tọa độ màn hình Canvas
        posXRatio = referenceRectTransform.localPosition.x / canvasRectTransform.rect.width;
        posYRatio = referenceRectTransform.localPosition.y / canvasRectTransform.rect.height;

        // Đặt kích thước bản thân của đối tượng
        SetObjectSize();
    }

    private void Update()
    {
        // Đặt kích thước bản thân của đối tượng
        SetObjectSize();
    }

    void SetObjectSize()
    {
        // Tính toán kích thước mới của đối tượng dựa trên tỉ lệ so với đối tượng tham chiếu
        float newHeight = canvasRectTransform.rect.height * scaleYRatio;
        float newWidth = newHeight * scale_reference_Ratio;

        // Đặt kích thước mới cho bản thân đối tượng
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(newWidth, newHeight);

        // Đặt tọa độ mới cho bản thân của đối tượng dựa trên tỷ lệ tính toán được
        Vector3 newPos = new Vector3(canvasRectTransform.rect.width * posXRatio, canvasRectTransform.rect.height * posYRatio, transform.localPosition.z);
        transform.localPosition = newPos;
    }
}
