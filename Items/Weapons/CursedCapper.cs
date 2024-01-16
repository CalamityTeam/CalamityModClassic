using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class CursedCapper : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/CursedCapper");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Cursed Capper");
        Item.damage = 30;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 44;
        Item.height = 32;
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2.25f;
        Item.value = 150000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = false;
        Item.shootSpeed = 14f;
        Item.shoot = Mod.Find<ModProjectile>("CursedRound").Type;
        Item.useAmmo = 97;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("CursedRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PhoenixBlaster);
        recipe.AddIngredient(ItemID.CursedFlame, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}