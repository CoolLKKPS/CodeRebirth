import json
import os

def main():
    json_path = "res/namespaced_keys.json"
    with open(json_path, 'r', encoding='utf-8') as f:
        data = json.load(f)
    
    type_map = {
        "DawnEnemyInfo": "Dawn.DawnEnemyInfo",
        "DawnWeatherEffectInfo": "Dawn.DawnWeatherEffectInfo",
        "DawnUnlockableItemInfo": "Dawn.DawnUnlockableItemInfo",
        "DawnItemInfo": "Dawn.DawnItemInfo",
        "DawnMapObjectInfo": "Dawn.DawnMapObjectInfo",
        "Dusk.DuskAchievementDefinition": "Dusk.DuskAchievementDefinition",
        "DawnMoonInfo": "Dawn.DawnMoonInfo",
    }
    
    output_lines = []
    output_lines.append("using Dawn;")
    output_lines.append("using Dusk;")
    output_lines.append("")
    
    for class_name, entries in data.items():
        type_name = entries.get("__type")
        if not type_name:
            print(f"Missing __type in {class_name}")
            continue
        generic_type = type_map.get(type_name)
        if not generic_type:
            print(f"Unknown type {type_name} for {class_name}")
            continue
        output_lines.append(f"public static class {class_name}")
        output_lines.append("{")
        for key, value in entries.items():
            if key == "__type":
                continue
            if isinstance(value, str):
                if ':' in value:
                    ns, k = value.split(':', 1)
                    output_lines.append(f'    public static NamespacedKey<{generic_type}> {key} => NamespacedKey<{generic_type}>.From("{ns}", "{k}");')
                else:
                    output_lines.append(f'    public static NamespacedKey<{generic_type}> {key} => NamespacedKey<{generic_type}>.From("code_rebirth", "{value}");')
            else:
                # should not happen
                pass
        output_lines.append("}")
        output_lines.append("")
    
    output = '\n'.join(output_lines)
    out_path = "src/CodeRebirthKeys.cs"
    with open(out_path, 'w', encoding='utf-8') as f:
        f.write(output)
    print(f"Generated {out_path}")

if __name__ == "__main__":
    main()
