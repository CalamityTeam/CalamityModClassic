using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Lacerator : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Lacerator");
		//Tooltip.SetDefault("Enemies that are close to the yoyo will have their life drained");
	}

    public override void SetDefaults()
    {
    	Item.CloneDefaults(ItemID.TheEyeOfCthulhu);
        Item.damage = 275;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.channel = true;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.knockBack = 7f;
        Item.value = 1000000;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("LaceratorProjectile").Type;
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
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		return false;
	}
}}