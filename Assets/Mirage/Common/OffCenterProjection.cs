using UnityEngine;

[ExecuteInEditMode]
public class OffCenterProjection : MonoBehaviour
{
    void LateUpdate()
    {
        var cam = GetComponent<Camera>();
        if (cam != null)
        {
            var near = cam.nearClipPlane;
            var top = near * Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView / 2);
            var right = cam.aspect * top;
            cam.projectionMatrix = CalculateProjectionMatrix(
                    -right, right, 0, top * 2, near, cam.farClipPlane);
        }
    }

    static Matrix4x4 CalculateProjectionMatrix(
        float left, float right, float bottom, float top,
        float near, float far)
    {
        var x = 2 * near / (right - left);
        var y = 2 * near / (top - bottom);
        var a = (right + left) / (right - left);
        var b = (top + bottom) / (top - bottom);
        var c = (far + near) / (near - far);
        var d = (2 * far * near) / (near - far);
        var e = -1.0f;
        var m = new Matrix4x4();
        m.SetRow(0, new Vector4(x, 0, a, 0));
        m.SetRow(1, new Vector4(0, y, b, 0));
        m.SetRow(2, new Vector4(0, 0, c, d));
        m.SetRow(3, new Vector4(0, 0, e, 0));
        return m;
    }
}
