using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AstrealDefeat : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/AstrealDefeat");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Astreal Defeat");
        Item.damage = 110;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 34;
        Item.height = 54;
        Item.useTime = 3;
        Item.useAnimation = 15;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        ////Tooltip.SetDefault("Fires Astreal Arrows\nEthereal bow of the tyrant king's mother\nThe mother strongly discouraged acts of violence throughout her life\nThough she kept this bow close to protect her family in times of great disaster");
        Item.knockBack = 5.5f;
        Item.value = 17500000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item102;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("AstrealArrow").Type; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 1f;
        Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
        Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("AstrealArrow").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.SpiritFlame);
        recipe.AddIngredient(ItemID.ShadowFlameBow);
        recipe.AddIngredient(null, "GreatbowofTurmoil");
        recipe.AddIngredient(null, "BladedgeGreatbow");
        recipe.AddIngredient(null, "DarkechoGreatbow");
        recipe.AddIngredient(null, "GalacticaSingularity", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}