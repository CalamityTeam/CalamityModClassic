using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class PlasmaRifle : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/PlasmaRifle");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Plasma Rifle");
        Item.damage = 100;
        Item.mana = 30;
        Item.DamageType = DamageClass.Magic;
        Item.width = 48;
        Item.height = 22;
        ////Tooltip.SetDefault("Fires a plasma blast that explodes");
        Item.useTime = 80;
        Item.useAnimation = 80;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4f;
        Item.value = 1050000;
        Item.rare = 9;
        Item.UseSound = new Terraria.Audio.SoundStyle("CalamityModClassic1Point1/Sounds/Item/PlasmaBlast");
        Item.autoReuse = true;
        Item.shootSpeed = 20f;
        Item.shoot = Mod.Find<ModProjectile>("PlasmaShot").Type;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("PlasmaShot").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 7);
        recipe.AddIngredient(ItemID.Musket);
        recipe.AddIngredient(ItemID.ToxicFlask);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 7);
        recipe.AddIngredient(ItemID.TheUndertaker);
        recipe.AddIngredient(ItemID.ToxicFlask);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}