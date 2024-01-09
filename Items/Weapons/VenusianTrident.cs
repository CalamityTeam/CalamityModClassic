using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class VenusianTrident : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Venusian Trident");
		//Tooltip.SetDefault("Casts an inferno bolt that erupts into a gigantic explosion of fire and magma shards");
		Item.staff[Item.type] = true;
	}

    public override void SetDefaults()
    {
        Item.damage = 155;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 20;
        Item.width = 48;
        Item.height = 48;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 9f;
        Item.value = 800000;
        Item.UseSound = SoundID.Item45;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("VenusianBolt").Type;
        Item.shootSpeed = 15f;
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 0);
            }
        }
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.InfernoFork);
        recipe.AddIngredient(null, "Phantoplasm", 5);
        recipe.AddIngredient(null, "TwistingNether");
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}