using UnityEngine;
using NUnit.Framework;
using System.Linq;
using UnityEditor.FilmTV.Toolbox;
using UnityEditor.SceneManagement;

class MaterialRemapperTest
{
    MaterialRemapperModel m_Model;

    [Test, Description("Test GetMeshCount returns the created meshes count")]
    public void TestMeshListCount() {
        Assert.That(m_Model.GetMeshCount() == 3);
    }

    [Test, Description("Test GetUniqueMeshList returns a list which size corresponds to unique meshes count")]
    public void TestUniqueMeshListCount() {
        Assert.That(m_Model.GetUniqueMeshList().Count == 2);
    }

    [SetUp]
    public void SetUp()
    {
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
        // Create two GOs from the same source mesh
        GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject.CreatePrimitive(PrimitiveType.Cube);
        // Create a GO from another mesh
        GameObject.CreatePrimitive(PrimitiveType.Sphere);

        m_Model = ScriptableObject.CreateInstance<MaterialRemapperModel>();
        m_Model.hideFlags = HideFlags.DontSave;

        var transforms = GameObject.FindObjectsOfType<MeshRenderer>().Select(X => X.transform);
        m_Model.FindMeshesAndMaterials(transforms.ToList());
    }
}
