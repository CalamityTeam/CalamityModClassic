using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AstralBlaster : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/AstralBlaster");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Astral Blaster");
        Item.damage = 40;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 40;
        Item.height = 24;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2.5f;
        Item.value = 150000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = true;
        Item.shootSpeed = 14f;
        Item.shoot = Mod.Find<ModProjectile>("AstralRound").Type;
        Item.useAmmo = 97;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("AstralRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VerstaltiteBar", 7);
        recipe.AddIngredient(ItemID.FallenStar, 3);
        recipe.AddIngredient(ItemID.SoulofLight);
        recipe.AddIngredient(ItemID.SoulofNight);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}