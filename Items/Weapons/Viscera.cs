using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Viscera : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Viscera");
		//Tooltip.SetDefault("The more tiles and enemies the beam bounces off of or travels through the more healing the beam does");
		Item.staff[Item.type] = true;
	}

    public override void SetDefaults()
    {
        Item.damage = 360;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 23;
        Item.width = 50;
        Item.height = 50;
        Item.useTime = 14;
        Item.useAnimation = 14;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 6f;
        Item.value = 1000000;
        Item.UseSound = SoundID.Item20;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("Viscera").Type;
        Item.shootSpeed = 6f;
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
        recipe.AddIngredient(null, "BloodstoneCore", 4);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}}