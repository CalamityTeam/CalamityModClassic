using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Head)]
public class GodSlayerHelm : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 5000000;
        Item.defense = 29; //96
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(43, 96, 222);
            }
        }
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("GodSlayerChestplate").Type && legs.type == Mod.Find<ModItem>("GodSlayerLeggings").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadow = true;
    }

    public override void UpdateArmorSet(Player player)
    {
    	CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
    	modPlayer.godSlayer = true;
        player.setBonus = "You will survive fatal damage and will be healed 300 HP if an attack would have killed you\n" +
        	"This effect can only occur once every 40 seconds\n" +
        	"While the cooldown for this effect is active you gain a 10% increase to all damage";
    }
    
    public override void UpdateEquip(Player player)
    {
    	CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
    	modPlayer.godSlayerDamage = true;
    	player.maxMinions += 3;
    	player.GetDamage(DamageClass.Melee) += 0.15f;
        player.GetCritChance(DamageClass.Melee) += 15;
        player.GetDamage(DamageClass.Ranged) += 0.15f;
        player.GetCritChance(DamageClass.Ranged) += 15;
        player.GetDamage(DamageClass.Magic) += 0.15f;
        player.GetCritChance(DamageClass.Magic) += 15;
        player.GetDamage(DamageClass.Throwing) += 0.15f;
        player.GetCritChance(DamageClass.Throwing) += 15;
        player.GetDamage(DamageClass.Summon) += 0.15f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CosmiliteBar", 14);
        recipe.AddIngredient(null, "NightmareFuel", 8);
        recipe.AddIngredient(null, "EndothermicEnergy", 8);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}