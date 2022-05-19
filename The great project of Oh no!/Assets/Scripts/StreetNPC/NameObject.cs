using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/NameObject")]
public class NameObject : ScriptableObject
{
    [SerializeField] private List<string> maleName;
    [SerializeField] private List<string> femaleName;
    [SerializeField] private List<string> mixedName;
    [SerializeField] private List<string> surname;
    public List<string> MaleName => maleName;
    public List<string> FemaleName => femaleName;
    public List<string> MixedName => mixedName;
    public List<string> Surname => surname;
}
